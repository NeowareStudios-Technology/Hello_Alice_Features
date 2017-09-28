/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/


using IBM.Watson.DeveloperCloud.DataTypes;
using IBM.Watson.DeveloperCloud.Services.SpeechToText.v1;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 414

namespace IBM.Watson.DeveloperCloud.Widgets
{
  /// <summary>
  /// Simple class for displaying the SpeechToText result data in the UI.
  /// </summary>
  /// 


  public class SpeechDisplayWidgetPhonetic : Widget
  {
    public Text tc;

    Animator anim;

    public GameObject myObject;
    public GameObject T, S, A;
    public static bool correct = false;
    bool connected = DetectCollision.activated;
    bool right = DetectCollision.gotright;
    string target1 = "sat";
    float begintime = 0;
    float endtime = 0;
    float correctcounter = 0;
    public Text Analysis;
    public Text CorrectCounterText;
    public Text Ratio;
    public Text ExampleText;
    bool completedCheck = false;
    float wordsperminute = 0;
    #region Inputs
    int a;
    [SerializeField]
    private Input m_SpeechInput = new Input("SpeechInput", typeof(SpeechToTextData), "OnSpeechInput");
    #endregion

    #region Widget interface
    /// <exclude />
    protected override string GetName()
    {
      return "SpeechDisplay";
    }
    #endregion

    #region Private Data

    [SerializeField]
    private bool m_ContinuousText = false;
    [SerializeField]
    private Text m_Output = null;
    [SerializeField]
    private InputField m_OutputAsInputField = null;
    [SerializeField]
    private Text m_OutputStatus = null;
    [SerializeField]
    private float m_MinConfidenceToShow = 0.2f;

    private string m_PreviousOutputTextWithStatus = "";
    private string m_PreviousOutputText = "";
    private float m_ThresholdTimeFromLastInput = 3.0f; //3 secs as threshold time. After 3 secs from last OnSpeechInput, we are considering input as new input
    private float m_TimeAtLastInterim = 0.0f;
    #endregion

    #region Event Handlers

    void Update()
    {
      connected = DetectCollision.activated;


    }
   
    private void OnSpeechInput(Data data)
    {
      
      /////////////////////////////These set the analytic texts//////////////////////////////////
      Analysis = GameObject.Find("Time#").GetComponent<Text>();
      CorrectCounterText = GameObject.Find("Correct#").GetComponent<Text>();
      Ratio = GameObject.Find("Ratio#").GetComponent<Text>();
      ExampleText = GameObject.Find("ExampleText").GetComponent<Text>();
      /////////////////////////////These detect if the cards have collided////////////////////////
      connected = DetectCollision.activated;
      print(connected);
      GameObject outputtext = GameObject.Find("Output");
      //////////////////////////Check for target word by comparing string/////////////////////////////////
      anim = GetComponent<Animator>();
      
      bool b;
      print("completed check " + completedCheck);
     if (completedCheck == false) { 
        if (connected == true)
        {
         
          DetectCollision.gotright = true;
         
          print("connected...");
          begintime = Time.time;
          //GameObject.Find("GotitRight").SetActive(false);


          b = m_Output.text.Contains(target1);
          
          print("text check: " + b);
          if (b == true)
          {


            print("correct");
            correct = true;

            //correctcounter++;
            endtime = Time.time;
            
            //float myFloat = Mathf.RoundToInt((float)endtime);
            wordsperminute = (correctcounter / (endtime / 60) + 1);

            print("got to here");


            //Changes the analytics text to display useful info

            correctcounter++;
            Analysis.text = (endtime.ToString("F2") + " s");

            Ratio.text = (wordsperminute.ToString() + " wpm");
            CorrectCounterText.text = (correctcounter.ToString());
            ExampleText.text = ("Alice SAT down.");
            AudioSource audio = GameObject.Find("ExampleText").GetComponent<AudioSource>();
            audio.Play();


            /*
            tc = outputtext.GetComponent<Text>();
            tc.color = Color.green;
            */

            myObject.SetActive(true);
            T.SetActive(false);
            A.SetActive(false);
            S.SetActive(false);
            //DetectCollision.activated = false;
            
            completedCheck = true;

          }
          }
        }
      

      /////////////////////////////////////////////////////////////////////////////////////////////////////
      
        SpeechRecognitionEvent result = ((SpeechToTextData)data).Results;

        if (result != null && result.results.Length > 0)

        {


          string outputTextWithStatus = "";
          string outputText = "";



          if (Time.time - m_TimeAtLastInterim > m_ThresholdTimeFromLastInput)
          {
            if (m_Output != null)

              m_PreviousOutputTextWithStatus = m_Output.text;


            if (m_OutputAsInputField != null)

              m_PreviousOutputText = m_OutputAsInputField.text;
          }

          if (m_Output != null && m_ContinuousText)

            outputTextWithStatus = m_PreviousOutputTextWithStatus;

          if (m_OutputAsInputField != null && m_ContinuousText)
            outputText = m_PreviousOutputText;


          foreach (var res in result.results)
          {
            foreach (var alt in res.alternatives)
            {
              string text = alt.transcript;
              if (m_Output != null)
              {



                m_Output.text = string.Concat(outputTextWithStatus, text);
              }


              if (m_OutputAsInputField != null)
              {

                if (!res.final || alt.confidence > m_MinConfidenceToShow)
                {

                  m_OutputAsInputField.text = string.Concat(outputText, " ", text);


                  if (m_OutputStatus != null)
                  {

                    m_OutputStatus.text = string.Format("{0}, {1:0.00}", res.final ? "Final" : "Interim", alt.confidence);
                  }
                }
              }

              if (!res.final)
                m_TimeAtLastInterim = Time.time;

            }
          }
        }
      }
    }
  }
  #endregion
