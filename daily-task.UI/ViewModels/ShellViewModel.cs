using Caliburn.Micro;

namespace daily_task.UI.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public ShellViewModel()
        {
            ActiveView.Parent = this;
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await base.OnActivateAsync(cancellationToken);

            await ActiveView.OpenItem<IndexViewModel>();
        }
    }
}