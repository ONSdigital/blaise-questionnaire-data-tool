namespace Blaise.Questionnaire.Data.Tool.Gui.Extensions
{
    using System.Windows.Forms;

    public static class FormExtensions
    {
        public static int? GetNullableIntegerValue(this TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                return null;
            }

            var successfullyParsed = int.TryParse(textBox.Text, out var integerValue);
            if (successfullyParsed)
            {
                return integerValue;
            }

            return null;
        }

        public static string GetNullableStringValue(this TextBox textBox)
        {
            return string.IsNullOrWhiteSpace(textBox.Text) ? null : textBox.Text;
        }

        public static string GetNullableStringValue(this ComboBox comboBox)
        {
            return string.IsNullOrWhiteSpace(comboBox.Text) ? null : comboBox.Text;
        }
    }
}
