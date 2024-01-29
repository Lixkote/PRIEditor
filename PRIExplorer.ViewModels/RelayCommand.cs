using System;
using System.Windows.Input;

namespace PRIExplorer.ViewModels;

public class RelayCommand : ICommand
{
	private Func<bool> canExecute;

	private Action execute;

	public event EventHandler CanExecuteChanged;

	public RelayCommand(Action execute)
	{
		this.execute = execute;
	}

	public RelayCommand(Func<bool> canExecute, Action execute)
	{
		this.canExecute = canExecute;
		this.execute = execute;
	}

	public bool CanExecute(object parameter)
	{
		if (canExecute != null)
		{
			return canExecute();
		}
		return true;
	}

	public void Execute(object parameter)
	{
		execute();
	}

	public void RaiseCanExecuteChanged()
	{
		this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}
}
internal class RelayCommand<T> : ICommand
{
	private Predicate<T> canExecute;

	private Action<T> execute;

	public event EventHandler CanExecuteChanged;

	public RelayCommand(Action<T> execute)
	{
		this.execute = execute;
	}

	public RelayCommand(Predicate<T> canExecute, Action<T> execute)
	{
		this.canExecute = canExecute;
		this.execute = execute;
	}

	public bool CanExecute(object parameter)
	{
		if (canExecute != null)
		{
			return canExecute((T)parameter);
		}
		return true;
	}

	public void Execute(object parameter)
	{
		execute((T)parameter);
	}

	public void RaiseCanExecuteChanged()
	{
		this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}
}
