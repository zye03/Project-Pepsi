﻿/******** Copyright (c) John P. Doran, All rights reserved. ******************
//
// File Name :  SimpleQTE.cs
// Author :     John P. Doran
//
// Purpose :    QTE with a single input. Works with either controller or 
//              keyboard input.
//
*****************************************************************************/
using UnityEngine;

namespace QTESystem
{
    public class SimpleQTE : QTE
    {
        
        [Header("Simple QTE Settings")]

        [Tooltip("The information used by the QTE in order to determine what input sprite to show and what button/key to press in order to succeed")]
        public InputData inputData;

        protected override void OnEnable()
        {
            base.OnEnable();

            switch(inputMode)
            {
                case InputMode.Controller:
                    input.sprite = inputData.controllerSprite;
                    break;
                case InputMode.Keyboard:
                    input.sprite = inputData.keyboardSprite;
                    break;
                case InputMode.Mobile:
                    input.sprite = inputData.mobileSprite;
                    break;
            }
        }

        protected virtual void Update()
        {
            switch (state)
            {
                case QTEState.Active:
                    {
                        if (inputData.IsDown())
                        {
                            QTESuccess();
                        }

                        break;
                    }
            }
        }
    }

}