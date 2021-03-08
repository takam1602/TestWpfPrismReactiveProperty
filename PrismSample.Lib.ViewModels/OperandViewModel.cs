using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;

using Reactive.Bindings;

using Prism.Events;
using Prism.Mvvm;

using Unity;

using PrismSample.Lib.Views;

namespace PrismSample.Lib.ViewModels
{
    public class OperandViewModel : BindableBase
    {
        [Required, Range(-10000, 10000)]
        public ReactiveProperty<string> Operand { get; }

     [Dependency]
        public IDialogHelper DialogHelper { get; set; }

        public ReactiveCommand<object> ShowDialogCommand { get; }

        //Constructor 
        public OperandViewModel(IEventAggregator eventAggregator)
        {
            Operand = new ReactiveProperty<string>("2")
                .SetValidateAttribute(() => Operand);
           
            ShowDialogCommand = new ReactiveCommand(Operand.ObserveHasErrors.Select(x => !x))
                .WithSubscribe(_ => DialogHelper.ShowDialog($"N = {Operand.Value}"));


            Observable.WithLatestFrom
            (
                Operand,
                Operand.ObserveHasErrors,
                (o, e) => (o, e)
            )
            .Where(z => !z.e)
            .Subscribe(z =>
            {
                eventAggregator
                    .GetEvent<PubSubEvent<double>>()
                    .Publish(double.Parse(z.o));
            });
        }
    }
}
