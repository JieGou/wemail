using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using Wemail.Common;
using Wemail.Common.User;
using Wemail.DAL;

namespace Wemail.ViewModels
{
    //TODO 回车按钮确认和ESC按钮退出
    //TODO 设置弹出对话框的风格
    //Done 已登录后，再点设置帐号 登录对话框应直接填充用户的信息
    //TODO 登录页面的确认按钮可用性 以及填充信息的数据检查
    public class UserLoginViewModel : BindableBase, IDialogAware
    {
        private UserModel _userModel;

        public UserModel UserModel
        {
            get { return _userModel; }
            set { SetProperty(ref _userModel, value); }
        }

        private DelegateCommand _loginCommand;
        private DelegateCommand _cancelCommand;

        public string Title => "用户登录";

        public DelegateCommand LoginCommand { get => _loginCommand = new DelegateCommand(LoginAction); }
        public DelegateCommand CancelCommand { get => _cancelCommand = new DelegateCommand(CancelAction); }

        private void CancelAction()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel, null));
        }

        private void LoginAction()
        {
            var userDto = HttpHelper.Login(UserModel.Account, UserModel.Passworld);

            if (userDto != null && !string.IsNullOrEmpty(userDto.Token))
            {
                var parameter = new DialogParameters();

                //2022-02-03 修改
                //设置用户 登录状态为true
                UserModel.SetUserLoginState(true);
                //把用户信息传为参数传出去
                parameter.Add("LoginStatus", UserModel);

                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, parameter));
            }
        }

        public UserLoginViewModel(IUser user)
        {
            _userModel = user as UserModel;
        }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}