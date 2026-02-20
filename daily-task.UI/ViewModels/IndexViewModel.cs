using Caliburn.Micro;
using daily_task.Application.Models;
using daily_task.Application.UseCases.Task.GetAllTasks;
using daily_task.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daily_task.UI.ViewModels
{
    public class IndexViewModel : Screen
    {
        private bool _loading;

        public bool Loading
        {
            get { return _loading; }
            set
            {
                _loading = value;
                NotifyOfPropertyChange(() => Loading);
            }
        }

        private string _totalGold = string.Empty;

        public string TotalGold
        {
            get { return _totalGold; }
            set
            {
                _totalGold = value;
                NotifyOfPropertyChange(() => TotalGold);
            }
        }

        public BindableCollection<TaskDisplayModel> Tasks { get; private set; } = new BindableCollection<TaskDisplayModel>();

        private readonly IGetAllTasksUseCase _getAllTasksUseCase;

        public IndexViewModel(IGetAllTasksUseCase getAllTasksUseCase)
        {
            _getAllTasksUseCase = getAllTasksUseCase;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            _ = LoadTasksAsync();
        }

        private async System.Threading.Tasks.Task LoadTasksAsync()
        {
            var tasks = await _getAllTasksUseCase.Execute();

            Tasks.Clear();
            Tasks.AddRange(tasks);
        }
    }
}
