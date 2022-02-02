using System.Collections.Generic;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using Wemail.Common;
using Wemail.Common.User;

namespace Wemail.Contact.ViewModels
{
    public class ContactViewModel : BindableBase, INavigationAware
    {
        private IDialogService _dialogService;
        private IUser _user;
        private ObservableCollection<string> _contacts;

        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ObservableCollection<string> Contacts
        {
            get => _contacts ?? (_contacts = new ObservableCollection<string>());
            set { SetProperty(ref _contacts, value); }
        }

        public ContactViewModel(IDialogService dialogService, IUser user)
        {
            _dialogService = dialogService;
            _user = user;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (!_user.IsLogin())
            {
                _dialogService.ShowDialog("UserLoginView", (r) =>
                {
                    var result = r.Result;
                    if (result == ButtonResult.OK)
                    {
                        //Done 接收传出的用户信息
                        var loginStatus = r.Parameters.GetValue<UserModel>("LoginStatus");
                        //_user.SetUserLoginState(loginStatus);

                        //直接赋传出来的用户值
                        _user = loginStatus;
                    }
                });

                if (!_user.IsLogin())
                {
                    return;
                }
            }
            InitData();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        private void InitData()
        {
            Message = "Wemail.Contact Prism Module";

            //2022-02-03 修改
            var contactList = new List<string>()
            {
                "联系人张某",
                "联系人王某",
            };
            _contacts = new ObservableCollection<string>(contactList);
            RaisePropertyChanged(nameof(Contacts));
        }
    }
}