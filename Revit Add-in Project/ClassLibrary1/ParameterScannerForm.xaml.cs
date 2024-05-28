using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Windows;

namespace ParameterScanner
{
    public partial class ParameterScannerForm : Window
    {
        private UIDocument _uiDoc;

        public ParameterScannerForm(UIDocument uiDoc)
        {
            InitializeComponent();
            _uiDoc = uiDoc;
        }

        private void IsolateElementsButton_Click(object sender, RoutedEventArgs e)
        {
            string paramName = parameterNameTextBox.Text;
            string paramValue = parameterValueTextBox.Text;
            IsolateOrSelectElements(paramName, paramValue, true);
        }

        private void SelectElementsButton_Click(object sender, RoutedEventArgs e)
        {
            string paramName = parameterNameTextBox.Text;
            string paramValue = parameterValueTextBox.Text;
            IsolateOrSelectElements(paramName, paramValue, false);
        }

        private void IsolateOrSelectElements(string paramName, string paramValue, bool isolate)
        {
            FilteredElementCollector collector = new FilteredElementCollector(_uiDoc.Document);
            List<Element> elements = new List<Element>();

            foreach (Element elem in collector)
            {
                Parameter param = elem.LookupParameter(paramName);
                if (param != null && (param.AsString() == paramValue || (string.IsNullOrEmpty(paramValue) && param.AsString() == "")))
                {
                    elements.Add(elem);
                }
            }

            if (isolate)
            {
                // Isolate elements
                _uiDoc.ActiveView.IsolateElementsTemporary(new List<ElementId>(elements.ConvertAll(e => e.Id)));
            }
            else
            {
                // Select elements
                _uiDoc.Selection.SetElementIds(new List<ElementId>(elements.ConvertAll(e => e.Id)));
            }

            MessageBox.Show($"{elements.Count} elements found with the parameter '{paramName}' and value '{paramValue}'.");
        }
    }
}
