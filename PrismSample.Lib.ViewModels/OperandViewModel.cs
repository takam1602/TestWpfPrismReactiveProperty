using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;
using Prism.Events;
using Prism.Mvvm;
using Reactive.Bindings;

namespace PrismSample.Lib.ViewModels
{
    public class OperandViewModel : BindableBase
    {
        [Required, Range(-10000, 10000)]
        public ReactiveProperty<string> Operand { get; }

        //Constructor 
        public OperandViewModel(IEventAggregator eventAggregator)
        {
            Operand = new ReactiveProperty<string>("2")
                .SetValidateAttribute(() => Operand);

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
