using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ProgrammersToolKit.Core;

namespace ProgrammersToolKit.Views
{
    public sealed partial class ApiTestResultDetailPage : Page
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
