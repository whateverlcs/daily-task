using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
