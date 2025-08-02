using Avalonia.Interactivity;
using Avalonia.Controls;
using Avalonia.Controls;
using ProgrammersToolKit.Core;

namespace ProgrammersToolKit.Views
{
    public partial class ApiTestResultDetailPage : UserControl
    {
        public ApiTestResultDetailPage(ApiTestResult result)
        {
            this.InitializeComponent();
            StatusCodeText.Text = result.StatusCode.ToString();
            HeadersBox.Text = result.ResponseHeaders;
            PrettyBodyBox.Text = result.ResponseBody;
            RawBodyBox.Text = result.ResponseBody;
            ErrorBox.Text = result.ErrorMessage;
        }
    }
}
