using System;
using System.Diagnostics;
using System.Windows.Automation;
using System.Windows.Forms;

namespace DetectBrowserUrl
{
    public partial class Form1 : Form
    {

       

        public Form1()
        {
            InitializeComponent();
        
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
                     
            //get all running process of firefox
            Process[] procsfirefox = Process.GetProcessesByName("firefox");
            foreach (Process firefox in procsfirefox)
            {
                //the firefox process must have a window
                if (firefox.MainWindowHandle == IntPtr.Zero)
                {
                    continue;
                }
                AutomationElement sourceElement = AutomationElement.FromHandle(firefox.MainWindowHandle);
                //works with latest version of firefox and for older version replace 'Search with Google or enter address' with this 'Search or enter address'     
                //or you can also find editbox element name using automation spy software
                AutomationElement editBox = sourceElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Search with Google or enter address"));             
                // if it can be found, get the value from the editbox
                if (editBox != null)
                {
                    ValuePattern val = ((ValuePattern)editBox.GetCurrentPattern(ValuePattern.Pattern));

                    Console.WriteLine("\n Firefox URL found: " + val.Current.Value);
                }

                //-----------------------------find titlebar element for site title---------------------------------// 
                
                AutomationElement elmTitleBar = sourceElement.FindFirst(TreeScope.Descendants,
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TitleBar));
                if (elmTitleBar != null)
                
                    Console.WriteLine("\n Firefox TitleBar found: " + elmTitleBar.Current.Name);
                }
            }
        }
    }


