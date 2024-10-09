using Cosmos.System;
using SoftOS.Graphics;
using SoftOS.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftOS.Apps
{
    public class Calculator : Proccess
    {
        public string[] buttonsText = new string[18] { "7", "8", "9", "/", "4", "5", "6", "*", "1", "2", "3", "-", "%", "0", "C", "+", "Del", "="};
        public List<Tuple<int, int, int, int, int>> buttonsCoords = new List<Tuple<int, int, int, int, int>>(); //mx> my> mx< my< btntxt
        string inputText = "", tmpText = "";
        float val1, val2, result;
        int operationType;
        bool operationSelected = false;
        public override void Run()
        {
            Window.DrawTop(this);
            int x = windowData.winPos.X;
            int y = windowData.winPos.Y;
            int sizeX = windowData.winPos.Width;
            int sizeY = windowData.winPos.Height;
            GUI.mainCanvas.DrawFilledRectangle(GUI.activeTheme.Item2, x, y + Window.topSize, sizeX, sizeY - Window.topSize);
            for(int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    GUI.mainCanvas.DrawFilledRectangle(GUI.colors.whiteColor, x + 5 + (j * 65), y + 5 + Window.topSize + (i * 65), 60, 60);
                    buttonsCoords.Add(Tuple.Create(x + 5 + (j * 65), y + 5 + Window.topSize + (i * 65), x + 65 + (j * 65), y + 65 + Window.topSize + (i * 65), j + (i * 4)));
                    GUI.mainCanvas.DrawString(buttonsText[j + (i * 4)], GUI.defaultFont, GUI.activeTheme.Item1, x + 30 + (j * 65), y + 30 + Window.topSize + (i * 65));
                }
            }
            for(int i = 0; i < 2; i++)
            {
                GUI.mainCanvas.DrawFilledRectangle(GUI.colors.whiteColor, x + 5 + (i * 130), y + 5 + Window.topSize + (4 * 65), 125, 60);
                buttonsCoords.Add(Tuple.Create(x + 5 + (i * 130), y + 5 + Window.topSize + (4 * 65), x + 130 + (i * 130), y + 65 + Window.topSize + (4 * 65), 16 + i));
                GUI.mainCanvas.DrawString(buttonsText[16 + i], GUI.defaultFont, GUI.activeTheme.Item1, x + 60 + (i * 130), y + 30 + Window.topSize + (4 * 65));
            }
            GUI.mainCanvas.DrawString(inputText, GUI.defaultFont, GUI.activeTheme.Item1, x + 130, y + 8 + Window.topSize + (5 * 65));
            Calculate();
        }
        void Calculate()
        {
            bool valueAdded = false, incremented = false;
            foreach (var btn in buttonsCoords)
            {
                if (GUI.mX > btn.Item1 && GUI.mX < btn.Item3 && GUI.mY > btn.Item2 && GUI.mY < btn.Item4)
                {
                    GUI.cursor = GUI.cursorBtn;
                    if (MouseManager.MouseState == MouseState.Left && !GUI.clicked && !incremented)
                    {
                        switch (btn.Item5)
                        {
                            case 3:         //divide /
                                operationType = 0;
                                tmpText = "";
                                operationSelected = true;
                                if(result == 0)
                                {
                                    val1 = float.Parse(inputText);
                                }
                                else
                                {
                                    val1 = result;
                                }
                                inputText = "";
                                break;
                            case 7:         //multiply *
                                operationType = 1;
                                tmpText = "";
                                operationSelected = true;
                                if (result == 0)
                                {
                                    val1 = float.Parse(inputText);
                                }
                                else
                                {
                                    val1 = result;
                                }
                                inputText = "";
                                break;
                            case 11:        //substract -
                                operationType = 2;
                                tmpText = "";
                                operationSelected = true;
                                if (result == 0)
                                {
                                    val1 = float.Parse(inputText);
                                }
                                else
                                {
                                    val1 = result;
                                }
                                inputText = "";
                                break;
                            case 12:        //modulo %
                                operationType = 4;
                                tmpText = "";
                                operationSelected = true;
                                if (result == 0)
                                {
                                    val1 = float.Parse(inputText);
                                }
                                else
                                {
                                    val1 = result;
                                }
                                inputText = "";
                                break;
                            case 14:        //clear C
                                val1 = val2 = result = 0;
                                tmpText = "";
                                inputText = "0";
                                operationSelected = false;
                                break;
                            case 15:        //addition +
                                operationType = 3;
                                tmpText = "";
                                operationSelected = true;
                                if (result == 0)
                                {
                                    val1 = float.Parse(inputText);
                                }
                                else
                                {
                                    val1 = result;
                                }
                                inputText = "";
                                break;
                            case 16:        //delete last digit Del
                                tmpText = "";
                                inputText = inputText.Substring(0, inputText.Length - 1);
                                if(inputText == "")
                                {
                                    inputText = "0";
                                }
                                break;
                            case 17:        //equal =
                                tmpText = "";
                                val2 = float.Parse(inputText);
                                inputText = GetResult(val1, val2, operationType);
                                result = float.Parse(GetResult(val1, val2, operationType));
                                break;
                            default:        //type digits 0-9
                                tmpText = buttonsText[btn.Item5];
                                break;
                        }
                        valueAdded = true;
                        incremented = true;
                        continue;
                    }
                }
                else
                {
                    GUI.cursor = GUI.cursorReg;
                }
            }
            if (valueAdded)
            {
                if(inputText == "0")
                {
                    inputText = "";
                }
                inputText += tmpText;
            }
        }

        string GetResult(float val1, float val2, int operation)
        {
            float result = 0;
            switch (operation)
            {
                case 0:
                    if(val2 != 0)
                    {
                        result = val1 / val2;
                        return (result).ToString();
                    }
                    else
                    {
                        return "ERR";
                    }
                case 1:
                    result = val1 * val2;
                    return result.ToString();
                case 2:
                    result = val1 - val2;
                    return result.ToString();
                case 3:
                    result = val1 + val2;
                    return result.ToString();
                case 4:
                    float quotient = val1 / val2;
                    float truncatedQuot = quotient >= 0 ? (int)quotient : (int)(quotient - 1);
                    result = val1 - truncatedQuot * val2;
                    return result.ToString();
                default:
                    return "";
            }
        }
    }
}
