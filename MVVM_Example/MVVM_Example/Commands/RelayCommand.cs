using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_Example.Commands
{
    /// <summary>
    /// Works with button's commands
    /// </summary>
    class RelayCommand : ICommand
    {
        /// <summary>
        /// Executes a command
        /// </summary>
        private Action<object> execute;

        /// <summary>
        /// Allows to manage the commands(can execute or not)
        /// </summary>
        private Predicate<object> canExecute;

        /// <summary>
        /// Event for changing an access to a command
        /// </summary>
        private event EventHandler CanExecuteChangedInternal;

        /// <summary>
        /// Constructor for creaing a command
        /// </summary>
        public RelayCommand(Action<object> execute)
            : this(execute, DefaultCanExecute)
        {
        }

        /// <summary>
        /// Constructor for creaing a command
        /// </summary>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Changes the access to the command
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                this.CanExecuteChangedInternal -= value;
            }
        }

        /// <summary>
        /// Shows if a command can be run
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return this.canExecute != null && this.canExecute(parameter);
        }

        /// <summary>
        /// Runs te command
        /// </summary>
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        /// <summary>
        /// Changes the access to the command
        /// </summary>
        public void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChangedInternal;
            if (handler != null)
            {;
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Destroys the command
        /// </summary>
        public void Destroy()
        {
            this.canExecute = _ => false;
            this.execute = _ => { return; };
        }

        /// <summary>
        /// The default access for a command
        /// </summary>
        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
    }
}
