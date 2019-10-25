using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using SeleniumFramework.Utilities.reportUtil;
using AventStack.ExtentReports.MarkupUtils;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;
using static SeleniumFramework.Utilities.hooks.TestListners;
using NUnit.Framework;
using System.Text.RegularExpressions;
using SeleniumFramework.Utilities.common;
using System.Reflection;

namespace SeleniumFramework.UIOperations
{
    public enum SelectionType
    {
        selectByValue,
        selectByIndex,
        selectByText
    }
    public class UIActions : GlobalVariables
    {
        public static int imageCounter = 0;
        //ExtentReportUtil extentReport = new ExtentReportUtil();

        public object IMakrkupHelper { get; private set; }
        public Actions Operations;

        #region EnterText
        /// <summary>
        /// Method to enter text in the input box
        /// </summary>
        /// <param name="element">Web element</param>
        /// <param name="textToEnter">Input string</param>
        /// <returns>Returns page object</returns>
        public UIActions EnterText(IWebElement element, string textToEnter)
        {
            ClearText(element);
            Wait(2);
            try
            {
                if (WaitForObject(element))
                {
                    if (!String.IsNullOrEmpty(textToEnter))
                    {
                        Common.GetElementAttributes(element);
                        element.SendKeys(textToEnter);
                        ExtentReportUtil.report.Log(Status.Pass, "Entered <b> " + textToEnter + "</b> in the element => " + GlobalVariables.ElementOperated);
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Unable to enter value <b>" + textToEnter + "</b> in text box");
                    }
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Object => " + element.GetAttribute("name") + " is not displayed or disabled");
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occurs in <b>EnterText</b> method =>" + ex.ToString());
            }
            return new UIActions();
        }

        #endregion

        #region ClearText

        /// <summary>
        /// Method to clear text in the input box
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Returns page object</returns>
        public UIActions ClearText(IWebElement element)
        {
            try
            {
                if (WaitForObject(element))
                {
                    Common.GetElementAttributes(element);
                    element.Clear();
                    ExtentReportUtil.report.Log(Status.Pass, "Text cleared from the input box => " + GlobalVariables.ElementOperated);
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Object is not displayed or disabled");
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception Occured on ClearText Method => " + ex.ToString());
            }
            return new UIActions();
        }
        #endregion

        #region Set_TextBox
        /// <summary>
        /// Method to clear text, Click on textbox, and enter text
        /// </summary>
        /// <param name="element">Web Element</param>
        /// <param name="InputData">Input string</param>
        /// <returns>Returns page object</returns>
        public UIActions Set_Textbox(IWebElement element, string InputData)
        {
            try
            {
                if (WaitForObject(element, 5))
                {         
                    //element.Clear();
                    element.Clear();
                    element.Click();     
                    element.SendKeys(InputData);
                    ExtentReportUtil.report.Log(Status.Pass, "Text cleared and entered <b>" + InputData + "</b> to the edit box => " + element.GetAttribute("name"));
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Unable to clear and enter <b>" + InputData + "</b> to the edit box => " + element.GetAttribute("name"));
                    TakeScreenshot();
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception Occured on Set_Textbox Method => " + ex.ToString());
                TakeScreenshot();
            }
            return new UIActions();
        }

        #endregion

        #region ClickOn
        /// <summary>
        /// To click on object/element
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Returns page object</returns>
        public UIActions ClickOn(IWebElement element)
        {
            try
            {
                if (WaitForObject(element))
                {
                    Common.GetElementAttributes(element);
                    element.Click();
                    ExtentReportUtil.report.Log(Status.Pass, "Clicked on object => " + GlobalVariables.ElementOperated);
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Object not found ");
                    TakeScreenshot();
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured on <b>ClickOn</B> method => " + ex.ToString());
                TakeScreenshot();
            }
            return new UIActions();
        }

        #endregion

        #region ClickOnJSByElement
        /// <summary>
        /// To click on object/element using JavaScript
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Returns page object</returns>
        public UIActions ClickOnJS(IWebElement element)
        {
            try
            {
                if (WaitForObject(element))
                {
                    Common.GetElementAttributes(element);
                    String js = "arguments[0].style.height='auto'; arguments[0].style.visibility='visible';";
                    ((IJavaScriptExecutor)driver).ExecuteScript(js, element);
                    element.Click();
                    ExtentReportUtil.report.Log(Status.Pass, "Clicked on object => " + GlobalVariables.ElementOperated);
                    WaitForObject(4);
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Object not found ");
                    TakeScreenshot();
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured on <b>ClickOn</B> method => " + ex.ToString());
                TakeScreenshot();
            }
            return new UIActions();
        }
        #endregion

        #region ClickOnJSByObject
        /// <summary>
        /// This function is to click on The object using BY Class[Id,class,XPATH]
        /// </summary>
        /// <param name="ByObject"></param>
        /// <returns></returns>

        public UIActions ClickOnJS(By ByObject)
        {
            try
            {
                if (WaitForObject(ByObject))

                {
                    IWebElement element = driver.FindElement(ByObject);
                    string elementText = GetObjectText(element);
                    String js = "arguments[0].style.height='auto'; arguments[0].style.visibility='visible';";
                    ((IJavaScriptExecutor)driver).ExecuteScript(js, element);
                    element.Click();
                    ExtentReportUtil.report.Log(Status.Pass, "Clicked on object => <b>" + elementText + "</b>");
                    WaitForObject(4);
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Object not found ");
                    TakeScreenshot();
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured on <b>ClickOn</B> method => " + ex.ToString());
                TakeScreenshot();
            }
            return new UIActions();
        }

        #endregion

        #region GetObjectText
        /// <summary>
        /// This function returns the text of the objec/element specified
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>

        public string GetObjectText(IWebElement element)
        {
            string elementTextByNameAttribute = null;
            string elementTextByText = null;
            string elementTextByTitleAttribute = null;
            string elementTextByClassAttribute = null;
            string result = null;
            try
            {
                if (WaitForObject(element))
                {
                    elementTextByNameAttribute = element.GetAttribute("name");
                    elementTextByText = element.Text.ToString();
                    elementTextByTitleAttribute = element.GetAttribute("title");
                    elementTextByClassAttribute = element.GetAttribute("class");
                    if (elementTextByNameAttribute != null && elementTextByNameAttribute != "")
                    {
                        result = elementTextByNameAttribute;
                    }
                    else if (elementTextByText != null && elementTextByText != "")
                    {
                        result = elementTextByText;
                    }
                    else if (elementTextByTitleAttribute != null && elementTextByTitleAttribute != "")
                    {
                        result = elementTextByTitleAttribute;
                    }
                    else
                    {
                        if (elementTextByClassAttribute.Contains("new-tile"))
                        {
                            result = "New Tile";
                        }
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }
        #endregion

        #region Click_EventByData
        /// <summary>
        /// To click on object if it has data(Xpath text)
        /// </summary>
        /// <param name="InputData">Input String</param>
        /// <returns>Returns page object</returns>
        public UIActions Click_EventByData(string InputData)
        {
            try
            {
                WaitForObject(5);
                IWebElement TargetElement = driver.FindElement(By.XPath("//*[contains(text(),'" + InputData + "')]"));
                TargetElement.Click();
                ExtentReportUtil.report.Log(Status.Pass, "Clicked on object with data <b>" + InputData + "</b>");
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured on Click_EventByData method => " + ex.ToString());
                TakeScreenshot();
            }
            return new UIActions();
        } 
        #endregion

        #region CheckElement_ByData
        /// <summary>
        /// To check element if it has data(provided XPATH InputData)
        /// </summary>
        /// <param name="InputData">Input String</param>
        /// <returns>Returns Bool value</returns>
        public bool CheckElement_ByData(string InputData)
        {
            try
            {
                WaitForObject(5);
                IWebElement TargetElement = driver.FindElement(By.XPath("//*[contains(text(),'" + InputData + "')]"));
                if (TargetElement.Displayed)
                {
                    ExtentReportUtil.report.Log(Status.Pass, "Checked Element is displayed with data <b>" + InputData + "</b>");
                    return true;
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Checked Element is not displayed with data " + InputData);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured on CheckElement_ByData method => " + ex.ToString());
                TakeScreenshot();
                return false;
            }
        } 
        #endregion

        #region CompareStrings
        /// <summary>
        /// To Compare strings on source and destination element are equal
        /// </summary>
        /// <param name="sourceElement">Web element</param>
        /// <param name="destinationElement">Web element</param>
        /// <returns>Returns Bool value</returns>
        public bool CompareStrings(IWebElement sourceElement, IWebElement destinationElement)
        {
            try
            {
                string sourceText, destinationText;
                if (WaitForObject(sourceElement) && WaitForObject(destinationElement))
                {
                    sourceText = sourceElement.Text;
                    destinationText = destinationElement.Text;

                    if (sourceText.Equals(destinationText))
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Source string <b>" + sourceText + "</b> and Destination string <b>" + destinationText + "</b> are equal");
                        return true;
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Source string <b>" + sourceText + "</b> and Destination string <b>" + destinationText + "</b> are NOT equal");
                        return false;
                    }
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Source element or Destination element NOT found");
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured on CompareStrings method => " + ex.ToString());
                TakeScreenshot();
                return false;
            }
        } 
        #endregion

        #region CompareSrcAttrValWith_DestText
        /// <summary>
        ///  To compare source element attribute value with destination element text
        /// </summary>
        /// <param name="sourceElement">Web element</param>
        /// <param name="destinationElement">Web element</param>
        /// <param name="SrcAtrribute">string SrcAtrribute</param>
        /// <returns>Returns Bool value</returns>
        public bool CompareSrcAttrValWith_DestText(IWebElement sourceElement, IWebElement destinationElement, string SrcAtrribute)
        {
            try
            {
                string sourceAttriValue, destinationText;
                if (WaitForObject(sourceElement) && WaitForObject(destinationElement))
                {
                    sourceAttriValue = sourceElement.GetAttribute(SrcAtrribute);
                    destinationText = destinationElement.Text;
                    if (sourceAttriValue.Equals(destinationText))
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Source attribute value <b>" + sourceAttriValue + "</b> and Destination string <b>" + destinationText + "</b> are equal");
                        return true;
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Source attribute <b>" + sourceAttriValue + "</b> and Destination string <b>" + destinationText + "</b> are NOT equal");
                        TakeScreenshot();
                        return false;
                    }
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Source element or Destination element NOT found");
                    TakeScreenshot();
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured on CompareSrcAttrValWith_DestText method => " + ex.ToString());
                TakeScreenshot();
                return false;
            }
        } 
        #endregion

        #region CheckElement_ByLocator_N_Data
        /// <summary>
        /// To check element text is equal with provided input data
        /// </summary>
        /// <param name="element">Web element</param>
        /// <param name="Data">String data</param>
        /// <returns>Returns Bool value</returns>
        public bool CheckElement_ByLocator_N_Data(IWebElement element, string Data)
        {
            string toCompare = string.Empty;
            bool result = false;
            try
            {
                if (WaitForObject(element, 10))
                {
                    toCompare = element.Text;
                    if (toCompare.Equals(Data))
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Checked Element text is equal with <b>" + Data + "</b>");
                        result = true;
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Checked Element text is not equal with " + Data);
                        TakeScreenshot();
                        result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured on CheckElement_ByLocator_N_Data method => " + ex.ToString());
                TakeScreenshot();
                result = false;
            }
            return result;
        } 
        #endregion

        #region ClickIfObjectExists
        /// <summary>
        /// To click on object if it exists
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Returns page object</returns>
        public UIActions ClickIfObjectExists(IWebElement element)
        {
            try
            {
                if (WaitForObject(element))
                {
                    Common.GetElementAttributes(element);
                    element.Click();
                    ExtentReportUtil.report.Log(Status.Pass, "Clicked on object => " + GlobalVariables.ElementOperated);
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Object not exists, hence not clicked");
                }
            }
            catch (ElementClickInterceptedException ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Element is obscured, unable to perform operation on it => " + ex.ToString());
            }
            return new UIActions();
        } 
        #endregion

        #region Get_LabelValue
        /// <summary>
        /// Method to get value(attribute "Value" text) from elements 
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Returns String value</returns>
        public string GetFieldValue(IWebElement element)
        {
            string elementValue = string.Empty;
            try
            {
                if (WaitForObject(element))
                {
                    string getValue = element.GetAttribute("value");
                    if (getValue != null)
                    {
                        elementValue = getValue.Trim();
                        ExtentReportUtil.report.Log(Status.Pass, "Label value => <b>" + elementValue + "</b>");
                        return elementValue;
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Warning, "Label value is null => " + elementValue);
                        return elementValue;
                    }
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Warning, "Element is not present => " + elementValue);
                    return elementValue;
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Element is obscured, unable to perform operation on it => " + ex.ToString());
                return elementValue;
            }
        } 
        #endregion

        #region Validate_InnerTextValue
        /// <summary>
        /// Method to check innertext(attribute "innertext" text) from element is equal or not
        /// </summary>
        /// <param name="element">Web element</param>
        /// <param name="InputData">String Inputdata</param>
        /// <returns>Returns Bool value</returns>

        public bool Validate_InnerTextValue(IWebElement element, string InputData)
        {
            try
            {
                if (WaitForObject(element, 5))
                {
                    string ValueToCompare = element.GetAttribute("innerText");
                    if (ValueToCompare.Trim().Equals(InputData.ToString()))
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Validated inner text => <b>" + InputData + "</b>");
                        return true;
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Innertext NOT Found Or Incorrect => " + InputData);
                        TakeScreenshot();
                        return false;
                    }
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Element NOT Found to Validate_InnerTextValue ");
                    TakeScreenshot();
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in Validate_InnerTextValue method => " + ex.ToString());
                TakeScreenshot();
                return false;
            }
        } 
        #endregion

        #region Validate_TextboxValue
        /// <summary>
        /// Method to check value(attribute "Value" text) from element is equal or not with provided Inputdata
        /// </summary>
        /// <param name="element">Web element</param>
        /// <param name="InputData">String Inputdata</param>
        /// <returns>Returns Bool value</returns>
        public bool Validate_TextboxValue(IWebElement element, string InputData)
        {
            try
            {
                if (WaitForObject(element, 5))
                {
                    string ValueToCompare = element.GetAttribute("value");
                    if (ValueToCompare.Trim() == InputData.ToString())
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Validated text value => <b>" + InputData + "</b>");
                        return true;
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Value NOT Found Or Incorrect => " + InputData);
                        TakeScreenshot();
                        return false;
                    }
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Element NOT Found to Validate_TextboxValue  ");
                    TakeScreenshot();
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in Validate_TextboxValue method => " + ex.ToString());
                TakeScreenshot();
                return false;
            }
        } 
        #endregion

        #region GetLabelText
        /// <summary>
        /// To get the text present on the web element(label)
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Returns String value</returns>
        public string GetLabelText(IWebElement element)
        {
            try
            {
                WaitForObject(5);
                if (WaitForObject(element))
                {
                    string LabelText = element.Text;
                    ExtentReportUtil.report.Log(Status.Pass, "Label Text => <b>" + LabelText + "</b>");
                    return LabelText;
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Element not available to get Label Text");
                    return null;
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in GetLabelText method " + ex.ToString());
                TakeScreenshot();
                return null;
            }
        } 
        #endregion

        #region Validate_Label
        /// <summary>
        /// Method to check element/label text from element is equal or not with provided Inputdata
        /// </summary>
        /// <param name="element">Web element</param>
        /// <param name="InputData">String Inputdata</param>
        /// <returns>Return Bool value</returns>
        public bool Validate_Label(IWebElement element, string InputData)
        {
            try
            {
                if (WaitForObject(element))
                {
                    string TargetStr = element.Text.Trim();
                    TargetStr = RemoveNonAsciiCharacters(TargetStr);
                    InputData = RemoveNonAsciiCharacters(InputData);
                    if (TargetStr.Trim() == InputData.Trim())
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Validated Label text => <b>" + InputData + "</b>");
                        return true;
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Label text NOT Found Or Incorrect => " + InputData);
                        TakeScreenshot();
                        return false;
                    }
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Element NOT Found to Validate_Label ");
                    TakeScreenshot();
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in Validate_Label method => " + ex.ToString());
                TakeScreenshot();
                return false;
            }
        } 
        #endregion

        #region Get_AttributeValue
        /// <summary>
        /// Method to get attribute text from element 
        /// </summary>
        /// <param name="element">Web element</param>
        /// <param name="attribute">Tag attribute</param>
        /// <returns>Return String value</returns>
        public string Get_AttributeValue(IWebElement element, string attribute)
        {
            try
            {
                if (WaitForObject(element))
                {
                    string getValue = element.GetAttribute(attribute);
                    if (getValue != null)
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Element attribute value => <b>" + getValue + "</b>");
                        return getValue.Trim();
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Element attribute value is => <b>NULL</b>");
                        TakeScreenshot();
                        return null;
                    }
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Element NOT Found to Get_AttributeValue ");
                    TakeScreenshot();
                    return null;
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in Get_AttributeValue method => " + ex.ToString());
                TakeScreenshot();
                return null;
            }
        } 
        #endregion

        #region VerifyColumnHeaders
        /// <summary>
        ///  Method to verify column headers are displayed or not 
        /// </summary>
        /// <param name="elements">List of Web elements</param>
        /// <returns>Return page object</returns>
        public UIActions VerifyColumnHeaders(IList<IWebElement> elements)
        {
            try
            {
                WaitForObject(5);
                foreach (IWebElement col in elements)
                {
                    if (col.Displayed)
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Column header is present => <b>" + col.Text.ToString() + "</b>");
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Column header is not present => ");
                        TakeScreenshot();
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in VerifyColumnHeaders method => " + ex.ToString());
                TakeScreenshot();
            }
            return new UIActions();
        } 
        #endregion

        #region Close_AlertIfAppears
        /// <summary>
        /// Method to close Alerts if appeared 
        /// </summary>
        /// <param name="element">Web element</param>
        public void Close_AlertIfAppears(IWebElement element)
        {
            try
            {
                if (element.Displayed)
                {
                    element.Click();
                    ExtentReportUtil.report.Log(Status.Pass, "Alert closed => <b>" + element.Text.ToString() + "</b>");
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Alert did not appeared ");
                    TakeScreenshot();
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in Close_AlertIfAppears method => " + ex.ToString());
                TakeScreenshot();
            }
        } 
        #endregion

        #region DeleteFilters
        /// <summary>
        /// Method to delete filters 
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Return page object</returns>
        public UIActions Delete_Filter(IWebElement element)
        {
            try
            {
                if (WaitForObject(element))
                {
                    element.SendKeys(Keys.ArrowDown);
                    element.SendKeys(Keys.ArrowDown);
                    element.SendKeys(Keys.Enter);
                    Thread.Sleep(2000);
                    ExtentReportUtil.report.Log(Status.Pass, "Filter deleted ");
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Filter element NOT foun ");
                    TakeScreenshot();
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in Delete_Filter method => " + ex.ToString());
                TakeScreenshot();
            }
            return new UIActions();
        } 
        #endregion

        #region DoubleClickUsingElement
        /// <summary>
        /// To double click on element
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Return page object</returns>
        public UIActions DoubleClick(IWebElement element)
        {
            try
            {
                Operations = new Actions(driver);
                if (WaitForObject(element))
                {
                    Common.GetElementAttributes(element);
                    Operations.DoubleClick(element).Perform();
                    ExtentReportUtil.report.Log(Status.Pass, "Double clicked on object => <b>" + GlobalVariables.ElementOperated + "</b>");
                    WaitForObject(10);
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Object not exists, hence not double clicked");
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Element is obscured, unable to perform operation on it => " + ex.ToString());
                TakeScreenshot();
            }
            return new UIActions();
        } 
        #endregion

        #region DoubleClickByData_N_Index
        /// <summary>
        ///  To Double click on element with given data and index
        /// </summary>
        /// <param name="data">Web element</param>
        /// <returns>Return page object</returns>
        public UIActions DoubleClickByData_N_Index(string data)
        {
            try
            {
                string[] data_N_Index = data.Split(',');
                string dataToClick = data_N_Index[0];
                int index = Convert.ToInt32(data_N_Index[1]);

                string element = "(//*[text()='" + dataToClick + "'])[" + index + "]";

                IWebElement TargetElement = driver.FindElement(By.XPath(element));
                Operations = new Actions(driver);
                if (WaitForObject(TargetElement))
                {
                    string elementText = GetObjectText(TargetElement);
                    Operations.DoubleClick(TargetElement).Perform();
                    SwitchToLastWindow();
                    ExtentReportUtil.report.Log(Status.Pass, "Double clicked on object => <b>" + elementText + "</b>");
                    WaitForObject(10);
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Object not exists, hence not double clicked");
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in Click_EventByData_N_Index method => " + ex.ToString());
                TakeScreenshot();
            }
            return new UIActions();
        } 
        #endregion

        #region RightClickWithContextMenu

        /// <summary>
        ///  To Right click on element and verify context menu displayed or not
        /// </summary>
        /// <param name="element">Web element</param>
        /// <param name="elementContextMenu">Web element</param>
        /// <returns>Return page object</returns>
        public UIActions RightClick(IWebElement element, IWebElement elementContextMenu)
        {
            RightClick(element);

            if (elementContextMenu.Displayed)
            {
                ExtentReportUtil.report.Log(Status.Pass, "ContextMenu displayed");
            }
            else
            {
                ExtentReportUtil.report.Log(Status.Fail, "Unable to display ContextMenu");
                TakeScreenshot();
            }
            return new UIActions();
        } 
        #endregion

        #region RightClick
        /// <summary>
        ///  To Right click on element
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Return page object</returns>
        public UIActions RightClick(IWebElement element)
        {
            try
            {
                WaitForObject(5);
                Operations = new Actions(driver);
                Common.GetElementAttributes(element);
                if (WaitForObject(element))
                {
                    Operations.ContextClick(element).Perform();
                    ExtentReportUtil.report.Log(Status.Pass, "Right click performed on => <b>" + GlobalVariables.ElementOperated + "</b>");
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Element not found to perform Right click operation");
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occure, Unable to perform Right click performed=> " + ex.ToString());
                TakeScreenshot();
            }
            return new UIActions();
        }

        #endregion

        #region RightClick_ByData
        /// <summary>
        ///  To Right click on element which has data present
        /// </summary>
        /// <param name="dataToClick">String dataToClick</param>
        /// <param name="elementContextMenu">Web element</param>
        /// <returns>Return page object</returns>
        public UIActions RightClick_ByData(string dataToClick, IWebElement elementContextMenu)
        {
            try
            {
                IWebElement TargetElement = driver.FindElement(By.XPath("//label[text()='" + dataToClick + "']"));
                if (WaitForObject(TargetElement))
                {
                    Operations = new Actions(driver);
                    Operations.ContextClick(TargetElement).Perform();

                    if (elementContextMenu.Displayed)
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Right click by data performed");
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Right click by data performed but Context menu did not displayed");
                        TakeScreenshot();
                    }
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Element not found to perform Right click by data");
                    TakeScreenshot();
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in RightClick_ByData method => " + ex.ToString());
                TakeScreenshot();
            }
            return new UIActions();
        } 
        #endregion

        #region RightClick_ByData_N_Index

        /// <summary>
        /// To Right click on object by data and index 
        /// </summary>
        /// <param name="data">String data</param>
        /// <param name="elementContextMenu">Web element</param>
        /// <returns>Return page object</returns>
        public UIActions RightClick_ByData_N_Index(string data, IWebElement elementContextMenu)
        {
            try
            {
                String[] data_N_Index = data.Split(',');
                String dataToClick = data_N_Index[0];
                int index = Convert.ToInt32(data_N_Index[1]);

                string element = "(//label[text()='" + dataToClick + "'])[" + index + "]";
                IWebElement TargetElement = driver.FindElement(By.XPath(element));
                if (WaitForObject(TargetElement))
                {
                    Operations = new Actions(driver);
                    Operations.ContextClick(TargetElement).Build().Perform();

                    if (elementContextMenu.Displayed)
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Right click by data and index performed");
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Right click by data and index performed but Context menu did not displayed");
                        TakeScreenshot();
                    }
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Element not found to perform Right click by data and index ");
                    TakeScreenshot();
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in RightClick_ByData_N_Index method => " + ex.ToString());
                TakeScreenshot();
            }
            return new UIActions();
        }

        #endregion

        #region Click_EventByData_N_Index
        /// <summary>
        ///  To click on object by data and index 
        /// </summary>
        /// <param name="data">String data</param>
        /// <returns>Returns page object</returns>
        public UIActions Click_EventByData_N_Index(string data)
        {
            try
            {
                String[] data_N_Index = data.Split(',');
                String dataToClick = data_N_Index[0];
                int index = Convert.ToInt32(data_N_Index[1]);

                string element = "(//*[text()='" + dataToClick + "'])[" + index + "]";

                IWebElement TargetElement = driver.FindElement(By.XPath(element));

                TargetElement.Click();
                ExtentReportUtil.report.Log(Status.Pass, "Click event by data and index performed");
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in Click_EventByData_N_Index method => " + ex.ToString());
                TakeScreenshot();
            }
            return new UIActions();
        } 
        #endregion

        #region Check_EventByData_N_Index
        /// <summary>
        /// To verify whether object is displayed by data
        /// </summary>
        /// <param name="data">String data</param>
        /// <returns>Return page object</returns>
        public UIActions Check_EventByData_N_Index(string data)
        {
            try
            {
                String[] data_N_Index = data.Split(',');
                String dataToClick = data_N_Index[0];
                int index = Convert.ToInt32(data_N_Index[1]);

                string element = "(//*[text()='" + dataToClick + "'])[" + index + "]";

                IWebElement TargetElement = driver.FindElement(By.XPath(element));
                if (TargetElement.Displayed)
                {
                    ExtentReportUtil.report.Log(Status.Pass, "Verified element diplayed by data ");
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in Check_EventByData_N_Index method => " + ex.ToString());
                TakeScreenshot();
            }
            return new UIActions();
        } 
        #endregion

        #region Check_EventByData
        /// <summary>
        /// To check element is enabled
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Return page object</returns>
        public UIActions Check_EventByData(string data)
        {
            try
            {
                string element = "(//*[contains(text(),'" + data + "')])";

                IWebElement TargetElement = driver.FindElement(By.XPath(element));
                if (TargetElement.Displayed)
                {
                    ExtentReportUtil.report.Log(Status.Pass, "Verified element diplayed by data ");
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in Check_EventByData method => " + ex.ToString());
                TakeScreenshot();
            }
            return new UIActions();
        } 
        #endregion

        #region CheckIfElementIsEnable
        /// <summary>
        /// To check element is enabled
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Return page object</returns>
        public UIActions CheckIfElementIsEnable(IWebElement element)
        {
            try
            {
                WaitForObject(5);
                if (WaitForObject(element))
                {
                    Common.GetElementAttributes(element);
                    if (element.Enabled)
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Element is Enabled=><b> " + GlobalVariables.ElementOperated + "</b>");
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Element is Disabled");
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in CheckIfElementIsEnable method " + ex.ToString());
                TakeScreenshot();
            }
            return new UIActions();
        }
        #endregion

        #region CheckIfElementIsPresent
        /// <summary>
        /// To check element(enabled/disabled) is present on web page
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Return Bool Value</returns>
        public bool CheckIfElementIsPresent(IWebElement element)
        {
            bool result = false;
            try
            {
                WaitForObject(5);
                if (WaitForObject(element))
                {
                    Common.GetElementAttributes(element);
                    if (element.Enabled)
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Element is Present =><b> " + GlobalVariables.ElementOperated + "</b>");
                        result = true;
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Element <b> " + GlobalVariables.ElementOperated + "</b> is not present on web page");
                        result = false;
                    }
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in CheckIfElementIsPresent() method " + ex.ToString());
                TakeScreenshot();
                return result;
            }
        }
        #endregion

        #region CheckIfElementIsAbsent
        /// <summary>
        /// To check element(enabled/disabled) is absent on web page
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Return Bool Value</returns>
        public bool CheckIfElementIsAbsent(IWebElement element)
        {
            bool result = false;
            try
            {
                WaitForObject(2);            
                Common.GetElementAttributes(element);
                ExtentReportUtil.report.Log(Status.Fail, "Element is Present =><b> " + GlobalVariables.ElementOperated + "</b>");
                TakeScreenshot();
                return false;
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Pass, "Element is NOT Present " + ex.ToString());
                return result;
            }
        }
        #endregion

        #region IsElementDisabled
        /// <summary>
        /// To check element is disabled
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Returns page object</returns>
        public UIActions IsElementDisabled(IWebElement element)
        {
            try
            {
                WaitForObject(15);
                if (WaitForObject(element))
                {
                    Common.GetElementAttributes(element);
                    if (element.Enabled)
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Element is Enabled =>" + GlobalVariables.ElementOperated);
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Element is Disabled ");
                    }
                }
            }
            catch (Exception ex)
            {
                TakeScreenshot();
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in CheckIfElementIsDisabled method " + ex.ToString());
            }
            return new UIActions();
        } 
        #endregion

        #region CheckIfElementSelected
        /// <summary>
        /// To check if element is selected
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Return page object</returns>
        public UIActions CheckIfElementSelected(IWebElement element)
        {
            try
            {
                if (WaitForObject(element))
                {
                    if (element.Selected)
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Element is Selected");
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Element is Unselected");
                    }
                }
            }
            catch (Exception ex)
            {
                TakeScreenshot();
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in CheckIfElementSelected method " + ex.ToString());
            }
            return new UIActions();
        } 
        #endregion

        #region RefreshBrowser
        /// <summary>
        /// To Refresh current Browser window
        /// </summary>
        /// <returns>Retun page object</returns>
        public UIActions RefreshBrowser()
        {
            try
            {
                driver.Navigate().Refresh();
                ExtentReportUtil.report.Log(Status.Pass, "Browser refreshed");
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Unable to refresh browser" + ex.ToString());
            }
            return new UIActions();
        } 
        #endregion

        #region MaximizeBrowserWindow
        /// <summary>
        /// To  Maximize Browser Window
        /// </summary>
        /// <returns>Retuns page object</returns>
        public UIActions MaximizeBrowserWindow()
        {
            try
            {
                driver.Manage().Window.Maximize();
                ExtentReportUtil.report.Log(Status.Pass, "Browser window Maximized");
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Unable to Maximized current window" + ex.ToString());
            }
            return new UIActions();
        } 
        #endregion

        #region RemoveNonAsciiCharacters
        /// <summary>
        /// To remove special characters from given string
        /// </summary>
        /// <param name="inputString">String inputString</param>
        /// <returns>Returns String value</returns>
        private string RemoveNonAsciiCharacters(string inputString)
        {
            return Regex.Replace(inputString, @"[^\u001F-\u007F]", string.Empty);
        } 
        #endregion

        #region Highlight
        /// <summary>
        ///  To Highlight the element/object
        /// </summary>
        /// <param name="element">Web element</param>
        public void Highlight(IWebElement element, IWebElement element2 = null)
        {
            for (int i = 0; i < 2; i++)
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, "border: 3px solid red;");
                if (element2 != null)
                {
                    js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element2, "border: 3px solid red;");
                }
                if (i == 1)
                {
                    TakeScreenshot();
                }
                js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, "");

            }
            ExtentReportUtil.report.Log(Status.Pass, "Element is Highlighted");
        }
        #endregion

        #region GetChildElementCount
        /// <summary>
        ///  To get child element count
        /// </summary>
        /// <param name="InputData">String Inputdata</param>
        /// <param name="element">List of Web elements</param>
        /// <returns>Retuns page object</returns>
        public UIActions GetChildElementCount(string InputData, List<IWebElement> element)
        {
            List<IWebElement> child = element.ToList();
            int childcount = child.Count();

            //for operationalareas count we need to -1 the count
            childcount = childcount - 1;
            if (InputData.Equals(childcount.ToString()))
            {
                ExtentReportUtil.report.Log(Status.Pass, "Get child element count");
            }
            else
            {
                TakeScreenshot();
                ExtentReportUtil.report.Log(Status.Pass, "Exception occured in GetChildElementCount method");
            }
            return new UIActions();
        }
        #endregion

        #region SendKeystrokes
        /// <summary>
        /// To send key stroke
        /// </summary>
        /// <param name="key2Send">String key2Send</param>
        /// <returns>Returns page object</returns>
        public UIActions SendKeystrokes(string key2Send)
        {
            try
            {
                if (key2Send == "PressDown")    
                {
                    //System.Windows.Forms.SendKeys.SendWait(Keys.ArrowDown);
                    SendKeystrokes(Keys.ArrowDown);
                    ExtentReportUtil.report.Log(Status.Pass, "Down key pressed");
                }
                else if (key2Send == "PressUp")
                {
                    //System.Windows.Forms.SendKeys.SendWait(Keys.ArrowUp);
                    SendKeystrokes(Keys.ArrowUp);
                    ExtentReportUtil.report.Log(Status.Pass, "Up key pressed");
                }
                else if (key2Send == "PressEnter")
                {
                    //System.Windows.Forms.SendKeys.SendWait(Keys.Enter);
                    ExtentReportUtil.report.Log(Status.Pass, "Enter key pressed");
                }
                else if (key2Send == "PressTab")
                {
                   // System.Windows.Forms.SendKeys.SendWait(Keys.Tab);
                    ExtentReportUtil.report.Log(Status.Pass, "Tab key pressed");
                }

                else if (key2Send == "PressControl")
                {
                    //System.Windows.Forms.SendKeys.SendWait(Keys.LeftControl);
                    ExtentReportUtil.report.Log(Status.Pass, "LeftControl key pressed");
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Invalid key pressed");
                }
            }
            catch (Exception ex)
            {
                TakeScreenshot();
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in SendKeystrokes method => " + ex.ToString());
            }
            return new UIActions();
        }
        #endregion

        #region InputFromKeyboard 
        /// <summary>
        /// To send Input key strokes
        /// </summary>
        /// <param name="KeyAction">String KeyAction</param>
        public void InputFromKeyboard(string KeyAction)
        {
            try
            {
                Operations = new Actions(driver);
                switch (KeyAction)
                {
                    case "PressDown":
                        Operations.SendKeys(Keys.Down).Build().Perform();
                        break;
                    case "PressEnter":
                        Operations.SendKeys(Keys.Enter).Build().Perform();
                        break;
                    case "PressTab":
                        Operations.SendKeys(Keys.Tab).Build().Perform();
                        break;
                    case "PressUp":
                        Operations.SendKeys(Keys.ArrowUp).Build().Perform();
                        break;
                    case "PressBackSpace":
                        Operations.SendKeys(Keys.Backspace).Build().Perform();
                        break;
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in InputFromKeyboard method => " + ex.ToString());
                TakeScreenshot();
            }
        }

        #endregion

        #region IsElementDisplayedNEnabled
        /// <summary>
        /// To check element is Displayed and Enabled
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Returns Bool value</returns>
        public bool IsElementDisplayedNEnabled(IWebElement element)
        {
            bool result = false;
            try
            {
                WaitForObject(10);
                if (WaitForObject(element))
                {
                    Common.GetElementAttributes(element);
                    if (element.Displayed && element.Enabled)
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Object is Displayed and Enabled => <b>" + GlobalVariables.ElementOperated + "</b>");
                        result = true;
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Skip, "Object is not Displayed or Enabled ");
                        TakeScreenshot();
                        result = false;
                    }
                    return result;
                }
                return result;
            }
            catch (StaleElementReferenceException ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Element is obscured, unable to perform operation on it => " + ex.ToString());
                TakeScreenshot();
                return false;
            }
        }
        #endregion

        #region IsWebElementDisabled
        /// <summary>
        /// To check element is Disabled
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Returns page object</returns>
        public UIActions IsWebElementDisabled(IWebElement element)
        {
            try
            {
                WaitForObject(10);
                if (WaitForObject(element))
                {
                    Common.GetElementAttributes(element);
                    if (element.Displayed)
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Object is Disabled => <b>" + GlobalVariables.ElementOperated + "</b>");
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Fail, "Object is Enabled => <b>" + GlobalVariables.ElementOperated + "</b>");
                        TakeScreenshot();
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Element is obscured, unable to perform operation on it => " + ex.ToString());
                TakeScreenshot();
            }
            return new UIActions();
        }
        #endregion

        #region IsEmptyElement
        /// <summary>
        /// To check element is Empty
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Returns page object</returns>
        public UIActions IsEmpty(IWebElement element)
        {
            try
            {
                WaitForObject(10);
                if (WaitForObject(element))
                {
                    //string elementText = GetObjectText(element);
                    if (element.GetAttribute("defaultValue") == "")
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Element => <b>" + element + "</b> is empty");
                    }
                    else
                    {
                        ExtentReportUtil.report.Log(Status.Pass, "Element => <b>" + element + "</b> is not empty and has Value - <b>" + element.GetAttribute("defaultValue") + "</b>");

                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Element is obscured, unable to perform operation on it => " + ex.ToString());
                TakeScreenshot();
            }
            return new UIActions();
        }


        #endregion

        #region IsElementNotDisplayed
        /// <summary>
        /// To check element object is Displayed
        /// </summary>
        /// <param name="ByObject">By WebObject</param>
        /// <returns>Returns Bool value</returns>
        public bool IsElementNotDisplayed(By ByObject)
        {
            bool flag = false;
            try
            {
                WaitForObject(10);
                if (driver.FindElement(ByObject).Displayed)
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Object is displayed ");
                    TakeScreenshot();
                    flag = false;

                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Pass, "Object is not displayed");
                    flag = true;

                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Element is obscured, unable to perform operation on it => " + ex.ToString());
                TakeScreenshot();
                flag = true;
            }
            return flag;
        }

        #endregion

        #region IsElementNotPresent
        /// <summary>
        /// To check element is not present
        /// </summary>
        /// <param name="ByObject">By WebObject</param>
        /// <returns>Returns Bool value</returns>
        public bool IsElementNotPresent(By ByObject)
        {
            try
            {
                driver.FindElement(ByObject);
                ExtentReportUtil.report.Log(Status.Fail, "Element is present");
                TakeScreenshot();
                return false;
            }
            catch (Exception)
            {
                ExtentReportUtil.report.Log(Status.Pass, "Element is not present");
                return true;
            }
        }

        #endregion

        #region IsElementDisplayed
        /// <summary>
        /// To check element is displayed 
        /// </summary>
        /// <param name="ByObject">By WebObject</param>
        /// <returns>Returns Bool value</returns>
        public bool IsElementDisplayed(By ByObject)
        {
            bool flag = false;
            try
            {
                WaitForObject(2);
                Common.GetElementAttributes(driver.FindElement(ByObject));
                if (driver.FindElement(ByObject).Displayed)
                {
                    ExtentReportUtil.report.Log(Status.Pass, "Object is displayed => <b>" + GlobalVariables.ElementOperated + "</b>");
                    flag = true;
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Object is not displayed ");
                    flag = false;
                    TakeScreenshot();
                }
            }
            catch (NoSuchElementException)
            {
                flag = false;
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Element is obscured, unable to perform operation on it => " + ex.ToString());
                TakeScreenshot();
                flag = false;
            }
            return flag;
        }

        #endregion

        #region OpenTile
        /// <summary>
        /// Method to open tile
        /// </summary>
        /// <param name="tile">String Tilename</param>
        /// <returns>Returns Page object</returns>
    

        #endregion

        #region SelectFilterOption
        /// <summary>
        /// Method to Select Filter
        /// </summary>
        /// <param name="FilterName">String FilterName</param>
        /// <returns>Returns page object</returns>
        public UIActions SelectFilterOption(string FilterName)
        {
            try
            {
                WaitForObject(5);
                IWebElement TargetElement = driver.FindElement(By.XPath("//div[@class='filter-button']//label[@title='" + FilterName + "'][text()='" + FilterName + "']"));
                WaitForObject(TargetElement);
                TargetElement.Click();
                Wait(2);
                ExtentReportUtil.report.Log(Status.Pass, "Clicked on Filter selected with data <b>" + FilterName + "</b>");
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured on SelectFilter method => " + ex.ToString());
                TakeScreenshot();
            }
            return new UIActions();
        }

        #endregion

   
        #region VerifySortingOfElementsAgainstAlist
        /// <summary>
        /// VerifySorting
        /// </summary>
        /// <param name="elements">List of Web elements</param>
        /// <param name="order">string order</param>
        /// <returns>Returns page object</returns>
        public UIActions VerifySorting(IList<IWebElement> elements, string order)
        {
            try
            {
                List<string> texts = new List<string>();
                List<string> orderedTexts = new List<string>();
                foreach (IWebElement element in elements)
                {
                    texts.Add(element.Text);
                }

                if (order.Equals("Ascending"))
                {
                    orderedTexts = texts.OrderBy(q => q).ToList();
                }
                else if (order.Equals("Descending"))
                {
                    orderedTexts = texts.OrderByDescending(q => q).ToList();
                }

                if (texts.Equals(orderedTexts))
                {
                    ExtentReportUtil.report.Log(Status.Pass, "Verified sorting ");
                }
            }
            catch (Exception ex)
            {
                TakeScreenshot();
                ExtentReportUtil.report.Log(Status.Fail, "Exception occurs VerifySorting method =>=> " + ex.ToString());
            }
            return new UIActions();
        }
        #endregion

        #region SwitchToFrameByIndex
        /// <summary>
        /// To switch frame by Index
        /// </summary>
        /// <param name="index">String Index</param>
        /// <returns>Returns page object</returns>
        public UIActions SwitchToFrameByIndex(string index)
        {
            try
            {
                int frameIndex = Convert.ToInt32(index);
                driver.SwitchTo().Frame(frameIndex);
                ExtentReportUtil.report.Log(Status.Pass, "Switched to the " + frameIndex + " Frame ");
            }
            catch (Exception ex)
            {
                TakeScreenshot();
                ExtentReportUtil.report.Log(Status.Fail, "Exception occures in the Switching to " + index + " Frame " + ex.ToString());
            }
            return new UIActions();
        }

        #endregion

        #region SwitchToFirstFrame
        /// <summary>
        /// To switch to the first frame
        /// </summary>
        /// <returns>Returns page object</returns>
        public UIActions SwitchToFirstFrame()
        {
            try
            {
                driver.SwitchTo().Frame(0);
                ExtentReportUtil.report.Log(Status.Pass, "Switched to the First Frame ");
            }
            catch (Exception ex)
            {
                TakeScreenshot();
                ExtentReportUtil.report.Log(Status.Fail, "Exception occures in the Switching to the First Frame " + ex.ToString());
            }
            return new UIActions();
        }

        #endregion

        #region SwitchToLastWindow
        /// <summary>
        /// To Switch to the Last Window
        /// </summary>
        /// <returns>Returns Page object</returns>
        public UIActions SwitchToLastWindow()
        {
            try
            {
                //TakeScreenshot();
                String LastWindow = driver.WindowHandles.Last();
                driver.SwitchTo().Window(LastWindow);
                WaitForObject(10);
                ExtentReportUtil.report.Log(Status.Pass, "Switched to the Last window ");
            }
            catch (NoSuchWindowException ex)
            {
                TakeScreenshot();
                ExtentReportUtil.report.Log(Status.Fail, "Exception occures in the Switching to the Last Window " + ex.ToString());
            }
            return new UIActions();
        }
        #endregion

        #region SwitchToFirstWindow
        /// <summary>
        /// To Switch to the First Window
        /// </summary>
        /// <returns>Returns Page object</returns>
        public UIActions SwitchToFirstWindow()
        {
            try
            {
                // TakeScreenshot();
                String FirstWindow = driver.WindowHandles.First();
                driver.SwitchTo().Window(FirstWindow);
                WaitForObject(10);
                ExtentReportUtil.report.Log(Status.Pass, "Switched to the First window ");
            }
            catch (NoSuchWindowException ex)
            {
                TakeScreenshot();
                ExtentReportUtil.report.Log(Status.Fail, "Exception occures in the Switching to the First Window " + ex.ToString());
            }
            return new UIActions();
        }
        #endregion

        #region SelectWindowUsingIndex
        /// <summary>
        /// To Switch to the Window by Index
        /// </summary>
        /// <param name="index">String Index</param>
        /// <returns>Returns Page object</returns>
        public UIActions SelectWindow(string index)
        {
            try
            {
                //TakeScreenshot();
                int ind = Convert.ToInt32(index);
                List<string> availableWindows = driver.WindowHandles.ToList();
                driver.SwitchTo().Window(availableWindows[ind]);
                ExtentReportUtil.report.Log(Status.Pass, "Switched to the selected window " + ind);
            }
            catch (Exception ex)
            {
                TakeScreenshot();
                ExtentReportUtil.report.Log(Status.Fail, "Exception occures in the Switching to the Selected Window " + ex.ToString());
            }
            return new UIActions();
        }

        #endregion

        #region CheckNumberofWindowsOpened
        /// <summary>
        /// To check the number of windows opened
        /// </summary>
        /// <returns>Returns Bool value</returns>
        public bool CheckNumberofWindowsOpened()
        {
            try
            {
                List<string> availableWindows = driver.WindowHandles.ToList();
                int CounWindows = availableWindows.Count;
                if (CounWindows == 1)
                {
                    ExtentReportUtil.report.Log(Status.Pass, "No new window opened");
                    return true;
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "New window opened <b>" + CounWindows + "</b>");
                    TakeScreenshot();
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occures in the CheckNumberofWindowsAreOpened Method " + ex.ToString());
                TakeScreenshot();
                return false;
            }
        }
        #endregion

        #region SelectWindow_BackToParent
        /// <summary>
        /// To switch the window back to the parent window
        /// </summary>
        /// <param name="index">String Index</param>
        /// <param name="element">Web element</param>
        /// <returns>Returns page object</returns>
        public UIActions SelectWindow_BackToParent(string index, IWebElement element)
        {
            try
            {
                // TakeScreenshot();
                string parentWindow = driver.CurrentWindowHandle;
                SelectWindow(index);
                driver.SwitchTo().Window(parentWindow);
                ExtentReportUtil.report.Log(Status.Pass, "Switched to the Back To Parent window ");
            }
            catch (Exception ex)
            {
                TakeScreenshot();
                ExtentReportUtil.report.Log(Status.Fail, "Exception occures in the Switching to the BackToParent Window " + ex.ToString());
            }
            return new UIActions();
        }
        #endregion

        #region SwitchToDefaultContentFromFrame
        /// <summary>
        /// To switch back to the Default content
        /// </summary>
        /// <returns>Returns page object</returns>
        public UIActions SwitchToDefaultContent()
        {
            try
            {
                //  TakeScreenshot();
                driver.SwitchTo().DefaultContent();
                ExtentReportUtil.report.Log(Status.Pass, "Switched to the Default window ");
            }
            catch (Exception ex)
            {
                TakeScreenshot();
                ExtentReportUtil.report.Log(Status.Fail, "Exception occures in the Switching to the Default Window " + ex.ToString());
            }
            return new UIActions();
        }
        #endregion

        #region Close_Window
        /// <summary>
        /// To close the all child windows of browser
        /// </summary>
        public void Close_Window()
        {
            try
            {
                driver.Close();
                if (driver.WindowHandles.Count > 1)
                {
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    driver.Close();
                }
                ExtentReportUtil.report.Log(Status.Pass, "All child windows have closed ");
            }
            catch (Exception ex)
            {
                TakeScreenshot();
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in Close_Window method =>" + ex.ToString());
            }
        }
        #endregion

        #region CloseCurrentWindow
        /// <summary>
        /// Close the browsers current window
        /// </summary>
        public void CloseCurrentWindow()
        {
            try
            {
                driver.Close();
                ExtentReportUtil.report.Log(Status.Pass, "Current window closed ");
            }
            catch (Exception ex)
            {
                TakeScreenshot();
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in CloseCurrentWindow method =>" + ex.ToString());
            }
        }

        #endregion

        #region AcceptAlert
        /// <summary>
        /// Accept the alert
        /// </summary>
        /// <returns>Returns page object</returns>
        public UIActions AcceptAlert()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                ExtentReportUtil.report.Log(Status.Pass, "Alert Accepted");
            }
            catch (Exception ex)
            {
                TakeScreenshot();
                ExtentReportUtil.report.Log(Status.Fail, "No Alert Present Exception => " + ex.ToString());
            }
            return new UIActions();
        }
        #endregion

        #region DismissAlert
        /// <summary>
        /// Dismiss the alert
        /// </summary>
        /// <returns>Returns page object</returns>
        public UIActions DismissAlert()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.Dismiss();
                ExtentReportUtil.report.Log(Status.Pass, "Alert Dismissed");
            }
            catch (Exception ex)
            {
                TakeScreenshot();
                ExtentReportUtil.report.Log(Status.Fail, "No Alert Present Exception => " + ex.ToString());
            }
            return new UIActions();
        }

        #endregion

        #region SelectFromDropDown
        /// <summary>
        /// To select from dropdown (This functions will only work with the Combo Boxes Or Drop-Down with Select Tag).
        /// </summary>
        /// <param name="selectionType">SelectionType selectionType</param>
        /// <param name="element">Web element</param>
        /// <param name="value">String value</param>
        /// <returns>Returns page object</returns>
        public UIActions SelectFromDropDown(SelectionType selectionType, IWebElement element, string value)
        {
            try
            {
                if (WaitForObject(element))
                {
                    Common.GetElementAttributes(element);
                    SelectElement select = new SelectElement(element);
                    switch (selectionType)
                    {
                        case SelectionType.selectByValue:
                            select.SelectByValue(value);
                            ExtentReportUtil.report.Log(Status.Pass, "Selected " + value + " from the " + GlobalVariables.ElementOperated + " dropdown by Value provided");
                            break;
                        case SelectionType.selectByText:
                            select.SelectByText(value);
                            ExtentReportUtil.report.Log(Status.Pass, "Selected " + value + " from the " + GlobalVariables.ElementOperated + " dropdown by Visible Text provided");
                            break;
                        case SelectionType.selectByIndex:
                            select.SelectByIndex(Convert.ToInt32(value));
                            ExtentReportUtil.report.Log(Status.Pass, "Selected " + value + " from the " + GlobalVariables.ElementOperated + " dropdown by Index provided");
                            break;
                    }
                }
                else
                {
                    TakeScreenshot();
                    ExtentReportUtil.report.Log(Status.Fail, "Drop-down object not found");
                }
            }
            catch (Exception ex)
            {
                TakeScreenshot();
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in SelectFromDropDown => " + ex.ToString());
            }
            return new UIActions();
        }
        #endregion

        #region MouseHover
        /// <summary>
        ///  This function is to hover on any control present on the page
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Returns page object</returns>
        public UIActions MouseHover(IWebElement element)
        {
            try
            {
                Operations = new Actions(driver);
                if (WaitForObject(element))
                {
                    Common.GetElementAttributes(element);
                    Operations.MoveToElement(element).Build().Perform();
                    ExtentReportUtil.report.Log(Status.Pass, "Mouse hovered on object - " + GlobalVariables.ElementOperated);
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Object not displayed on the screen");
                    TakeScreenshot();
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured on MouseHover => " + ex.ToString());
            }
            return new UIActions();
        }
        #endregion

        #region MouseDoubleClick
        /// <summary>
        /// To perfoem mouse double click on element
        /// </summary>
        /// <param name="element">Web element</param>
        /// <returns>Returns page object</returns>
        public UIActions MouseDoubleClick(IWebElement element)
        {
            try
            {
                Operations = new Actions(driver);
                if (WaitForObject(element))
                {
                    Common.GetElementAttributes(element);
                    Operations.MoveToElement(element).DoubleClick().Build().Perform();
                    ExtentReportUtil.report.Log(Status.Pass, "Mouse hovered and double clicked on object - " + GlobalVariables.ElementOperated);
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Object not displayed on the screen");
                    TakeScreenshot();
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured on MouseHover => " + ex.ToString());
            }
            return new UIActions();
        }
        #endregion

        #region DragAndDrop
        /// <summary>
        /// To perform Drag and Drop operation on web page
        /// </summary>
        /// <param name="source">Web element</param>
        /// <param name="target">Web element</param>
        /// <returns>Returns page object</returns>
        public UIActions DragAndDrop(IWebElement source, IWebElement target)
        {
            try
            {
                Operations = new Actions(driver);
                if (WaitForObject(source) && WaitForObject(target))
                {
                    string SourceObject = Common.GetElementAttributes(source);
                    string TargetObject = Common.GetElementAttributes(target);
                    Operations.ClickAndHold(source);
                    Operations.MoveToElement(target).Release();
                    Operations.Build().Perform();
                    ExtentReportUtil.report.Log(Status.Pass, "Performed Drag & Drop operation from " + SourceObject + " to " + TargetObject + " successfully");
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Source or Target element not loaded on the page");
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in DragAndDrop operation => " + ex.ToString());
            }
            return new UIActions();
        }
        #endregion

        #region FileUpload
        /// <summary>
        /// To upload file (Using SendKeys(FilePath))
        /// </summary>
        /// <param name="element">Web element</param>
        /// <param name="FilePath">String Filepath</param>
        /// <returns>Returns page object</returns>
        public UIActions FileUpload(IWebElement element, string FilePath)
        {
            try
            {
                if (WaitForObject(element))
                {
                    element.SendKeys(FilePath);
                    ExtentReportUtil.report.Log(Status.Pass, "File - " + FilePath + " uploaded successfully.");
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Object not found on the page");
                }

            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in File Upload => " + ex.ToString());
            }

            return new UIActions();
        }
        #endregion

        #region WaitForObject
        /// <summary>
        /// To wait for element until(Within Provided Timeout) it Displayed 
        /// </summary>
        /// <param name="element">Web element</param>
        /// <param name="TimeOut">Int Timeout</param>
        /// <returns>Returns Bool value</returns>
        public bool WaitForObject(IWebElement element, int TimeOut = 30)
        {
            bool flag = false;
            for (int i = 0; i < TimeOut; i++)
            {
                try
                {
                    if (element.Displayed)
                    {
                        flag = true;
                        ExtentReportUtil.report.Log(Status.Pass, "Element displayed");
                        break;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception)
                {
                }
            }
            return flag;
        }
        #endregion

        #region WaitForObjectByElement
        /// <summary>
        /// To wait for object until(Within Provided Timeout) it Displayed 
        /// </summary>
        /// <param name="element">By elementObject</param>
        /// <param name="TimeOut">Int Timeout</param>
        /// <returns>Returns Bool value</returns>
        public bool WaitForObject(By element, int TimeOut = 30)
        {
            bool flag = false;
            for (int i = 0; i < TimeOut; i++)
            {
                try
                {
                    if (driver.FindElement(element).Displayed)
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                }
            }
            return flag;
        }
        #endregion

        #region WaitExplicitUsingSeconds
        /// <summary>
        /// To Wait for webelement Explicitely(Hard wait)
        /// </summary>
        /// <param name="TimeOutInSeconds">Int TimeOutInSeconds</param>
        /// <returns>Returns page object</returns>
        public UIActions Wait(int TimeOutInSeconds)
        {
            try
            {
                Thread.Sleep(TimeOutInSeconds * 1000);
                ExtentReportUtil.report.Log(Status.Pass, "Waited for <b>" + TimeOutInSeconds + "</b> seconds");
                return new UIActions();
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured on Wait method => " + ex.ToString());
                return new UIActions();
            }
        }
        #endregion

        #region WaitForObjectImplicit
        /// <summary>
        /// To wait for page load(Within Provided Timeout) 
        /// </summary>
        /// <param name="TimeOut">Int Timeout</param>
        public void WaitForObject(int TimeOut = 30)
        {
            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TimeOut);
            }
            catch (Exception)
            {
                Thread.Sleep(2000);
            }
        }
        #endregion

        #region ScrollElementIntoView
        /// <summary>
        /// To scroll to the web element
        /// </summary>
        /// <param name="element">Web element</param>
        public void ScrollElementIntoView(IWebElement element)
        {
            try
            {
                Common.GetElementAttributes(element);
                WaitForObject(5);
                var js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'center'})", element);
                ExtentReportUtil.report.Log(Status.Pass, "Scrolled to the element <b>" + GlobalVariables.ElementOperated + "</b>");
            }
            catch (Exception)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Unable to scroll to the element");
            }
        }
        #endregion

        #region VerifyAttribute
        /// <summary>
        /// To check Elements attribute value with provided expected value
        /// </summary>
        /// <param name="element">Web element</param>
        /// <param name="attributeName">String AttributeName</param>
        /// <param name="expectedValue">String ExpectedValue</param>
        /// <returns>Returns Bool Value</returns>
        public bool VerifyAttribute(IWebElement element, string attributeName, string expectedValue)
        {
            bool flag = false;
            try
            {
                if (WaitForObject(element))
                {
                    flag = element.GetAttribute(attributeName).Equals(expectedValue);
                    //ExtentReportUtil.report.Log(Status.Pass, "Attribute value <b> " + expectedValue + " </b> is matching as expected");
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Verify Attribute, Object not found on the page ");
                }
                if (flag)
                {
                    ExtentReportUtil.report.Log(Status.Pass, "Attribute value <b> " + expectedValue + " </b> is matching as expected");
                }
                else {
                    ExtentReportUtil.report.Log(Status.Fail, "Verify Attribute, Object not found on the page ");
                }
                return flag;
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured on VerifyAttribute => " + ex.ToString());
                return flag;
            }
        }
        #endregion

        #region VerifyTextFromLabelElement
        /// <summary>
        /// To check element text with provided expected value 
        /// </summary>
        /// <param name="element">Web element</param>
        /// <param name="expectedValue">String expectedValue</param>
        /// <returns>Returns Bool Value</returns>
        public bool VerifyTextFromLabelElement(IWebElement element, string expectedValue)
        {
            bool flag = false;
            try
            {
                if (WaitForObject(element))
                {
                    flag = element.Text.Equals(expectedValue);
                    ExtentReportUtil.report.Log(Status.Pass, "VerifyTextFromLabelElement, Label Text is as Expected ");
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "VerifyTextFromLabelElement, Label Text is NOT as Expected ");
                }
                return flag;
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured on VerifyTextFromLabelElement => " + ex.ToString());
                return flag;
            }
        }
        #endregion

        #region TakeScreenshot
        /// <summary>
        /// To capture screenshot of current window
        /// </summary>
        /// <returns>Returns page object</returns>
        public UIActions TakeScreenshot()
        {
            try
            {
                imageCounter++;

                Screenshot screenshot = ((ITakesScreenshot)GlobalVariables.driver).GetScreenshot();

                if (GlobalVariables.ScreenshotType.Equals("External"))
                {
                    string screenshotName = GlobalVariables.logReportFolder + "\\screenshots\\image" + imageCounter + ".PNG";
                    screenshot.SaveAsFile(screenshotName, ScreenshotImageFormat.Png);
                    ExtentReportUtil.report.Log(Status.Info, "<a href = 'file:///" + screenshotName + "'  target='_blank'> Screenshot Taken </a>");
                }
                else if (GlobalVariables.ScreenshotType.Equals("Embedded"))
                {
                    string screenshotBase64 = screenshot.AsBase64EncodedString;
                    string imageURL = "data:image/png;base64," + screenshotBase64;
                    ExtentReportUtil.report.Log(Status.Info, "<a href = '" + imageURL + "' target='_blank'> Screenshot Taken </a>");
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured while taking screenshot => " + ex.ToString());
            }
            return new UIActions();
        }

        #endregion

        #region AssertTrue
        /// <summary>
        /// To ASSERT TRUE
        /// </summary>
        /// <param name="flag">Bool Flag</param>
        /// <returns>Returns page object</returns>
        public UIActions AssertTrue(bool flag)
        {
            switch (flag)
            {
                case true:
                    ExtentReportUtil.Test.Log(Status.Pass, "Assertion passed for => " + TestContext.CurrentContext.Test.MethodName);
                    try
                    {
                        Assert.Pass();
                    }
                    catch (SuccessException) { }
                    break;
                case false:
                    ExtentReportUtil.Test.Log(Status.Fail, "Assertion faild for => " +
                        TestContext.CurrentContext.Test.MethodName);
                    Assert.Fail();
                    TakeScreenshot();
                    break;
                default:
                    break;
            }
            return new UIActions();
        }
        #endregion

        /*  public UIActions AssertTrue(bool flag,string ErrorMsg)
          {
              switch (flag)
              {
                  case true:
                      ExtentReportUtil.Test.Log(Status.Pass, "Assertion passed for => " + TestContext.CurrentContext.Test.MethodName);
                      try
                      {
                          Assert.Pass();
                      }
                      catch (SuccessException) { }
                      break;
                  case false:
                      ExtentReportUtil.Test.Log(Status.Fail, "Assertion faild for => " +
                          TestContext.CurrentContext.Test.MethodName);
                      Assert.Fail();
                      TakeScreenshot();
                      break;
                  default:
                      break;
              }
              return new UIActions();
          }*/

        #region AssertFalse
        /// <summary>
        /// To ASSERT FALSE
        /// </summary>
        /// <param name="flag">Bool Flag</param>
        /// <returns>Returns page object</returns>
        public UIActions AssertFalse(bool flag)
        {
            switch (flag)
            {
                case true:
                    ExtentReportUtil.Test.Log(Status.Fail, "Assertion failed for => " + TestContext.CurrentContext.Test.MethodName);
                    Assert.Fail();
                    break;
                case false:
                    ExtentReportUtil.Test.Log(Status.Pass, "Assertion passed for => " +
                        TestContext.CurrentContext.Test.MethodName);
                    try
                    {
                        Assert.Pass();
                    }
                    catch (SuccessException) { }
                    break;
                default:
                    break;
            }
            return new UIActions();
        }
        #endregion

        #region VerifyPageLoaded
        /// <summary>
        /// To verify page is loaded 
        /// </summary>
        /// <param name="Element">Web element</param>
        /// <param name="PageName">String pageName</param>
        /// <returns>Returns Bool Value</returns>
        public bool VerifyPageLoaded(By Element, string PageName)
        {
            try
            {
                if (WaitForObject(Element))
                {
                    ExtentReportUtil.report.Log(Status.Pass, "<b>" + PageName + "</b> Page Loaded Successfully");
                    return true;
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Failed to open <b>" + PageName + "</b> ");
                    TakeScreenshot();
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured in VerifyPageLoaded() method => " + ex.ToString());
                TakeScreenshot();
                return false;
            }
        }
        #endregion

        #region CompareTwoStrings
        /// <summary>
        /// To compare Source and Destination string
        /// </summary>
        /// <param name="sourceText">String sourceText</param>
        /// <param name="destinationText">String destinationText</param>
        /// <returns>Returns Bool value</returns>
        public bool CompareTwoStrings(string sourceText, string destinationText)
        {
            try
            {
                if (sourceText.Equals(destinationText))
                {
                    ExtentReportUtil.report.Log(Status.Pass, "Source string <b>" + sourceText + "</b> and Destination string <b>" + destinationText + "</b> are equal");
                    return true;
                }
                else
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Source string <b>" + sourceText + "</b> and Destination string <b>" + destinationText + "</b> are NOT equal");
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExtentReportUtil.report.Log(Status.Fail, "Exception occured on CompareStrings method => " + ex.ToString());
                TakeScreenshot();
                return false;
            }
        }
        #endregion

        public bool ElementNotPresent(string XpathLocator)
        {
            bool flag = false;
            for (int i = 0; i < 30; i++)
            {
                try
                {
                    if (driver.FindElements(By.XPath(XpathLocator)).Count == 0)
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception e)
                {
                    ExtentReportUtil.report.Log(Status.Fail, "Exception occured on WaitForObject: " + e.ToString());
                }
            }
            //IList<IWebElement> listOfElements = driver.FindElements(By.XPath(XpathLocator));
            //int numberOfElementsFound = listOfElements.Count();
            //if (numberOfElementsFound == 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return flag;
        }

        
    }
}
