using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wemail.Controls.CustomContorls
{
    public class MessageDialogControl : BindableBase, IDialogAware
    {
        private DelegateCommand _getMessageCommand;
        private DelegateCommand _cancelMessageCommand;
        private string _messageContent;

        public string MessageContent
        {
            get => _messageContent;
            set
            {
                _messageContent = value;
                SetProperty(ref _messageContent, value);
            }
        }

        /// <summary>
        /// 确定命令
        /// </summary>
        public DelegateCommand GetMessageCommand
        {
            get => _getMessageCommand = new DelegateCommand(() =>
            {
                var parameter = new DialogParameters();
                parameter.Add("MessageContent", MessageContent);

                //关闭 并回传参数回去
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, parameter));
            });
        }

        /// <summary>
        /// 取消命令
        /// </summary>
        public DelegateCommand CancelMessageCommand
        {
            get => _cancelMessageCommand = new DelegateCommand(() =>
            {
                RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
            });
        }

        public string Title => "Message";

        public event Action<IDialogResult> RequestClose;

        /// <summary>
        /// 允许用户手动关闭当前窗口
        /// </summary>
        /// <returns></returns>
        public bool CanCloseDialog()
        {
            return true;
        }

        /// <summary>
        /// 关闭dialog的操作
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void OnDialogClosed()
        {
        }

        /// <summary>
        /// dialog接收参数传递
        /// </summary>
        /// <param name="parameters"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnDialogOpened(IDialogParameters parameters)
        {
            //接收传递到对话框的参数
            var parameterContent = parameters.GetValue<string>("Value");
            if (string.IsNullOrEmpty(parameterContent))
            {
                return;
            }

            MessageContent = parameterContent;
        }
    }
}