using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ViewModel
{
    public class ActionManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private AbstractModelAPI modelAPI;
        private int BallQuantity = 1;
        private int BallRadius = 30;
        private bool isEnabled = true;
        private ObservableCollection<Circle> circles;

        public ActionManager() : this(null) { }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public String Balls
        {
            get { return Convert.ToString(BallQuantity); }
            set
            {
                try
                {
                    BallQuantity = Convert.ToInt32(value);
                }
                catch
                {
                    BallQuantity = 0;
                }
            }
        }

        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                OnPropertyChanged("IsEnabled");
                OnPropertyChanged("IsDisabled");
            }
        }

        public ObservableCollection<Circle> Circles
        {
            get => circles;
            set
            {
                if (value.Equals(circles)) return;
                circles = value;
                OnPropertyChanged("Circles");
            }
        }

        public ActionManager(AbstractModelAPI modelAPI = null)
        {
            EnableAction = new Action(Enable);
            DisableAction = new Action(Disable);
            if (modelAPI == null)
            {
                this.modelAPI = AbstractModelAPI.CreateAPI();
            }
            else
            {
                this.modelAPI = modelAPI;
            }
        }
        private void Enable()
        {
            modelAPI.CreateBoard(500, 666, BallQuantity, BallRadius);
            modelAPI.CreateBalls();
            modelAPI.Enable();
            isEnabled = true;
            Circles = modelAPI.GetCirclesList();
        }

        private void Disable()
        {
            modelAPI.Disable();
            isEnabled = false;
        }

        public ICommand EnableAction { get; set; }
        public ICommand DisableAction { get; set; }
    }
}
