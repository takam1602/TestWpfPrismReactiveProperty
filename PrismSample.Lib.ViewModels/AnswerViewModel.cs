using Prism.Events;
using Prism.Mvvm;
using Reactive.Bindings;
using PrismSample.Lib.Models;
using Unity;

namespace PrismSample.Lib.ViewModels
{
    public class AnswerViewModel : BindableBase
    {
        public ReactiveProperty<string> Answer { get; }

        public ReactiveCommand<object> ShowDialogCommand { get; }
        public IModel model { get; set; }

        public AnswerViewModel(IEventAggregator eventAggregator)
        {
            Answer = new ReactiveProperty<string>("4");
            //should be constructed ???? or no need??
            model = new Model();

            eventAggregator
                .GetEvent<PubSubEvent<double>>()
                .Subscribe(CalculateAnswer);
        }

        private void CalculateAnswer(double operand)
        {
            //Answer.Value = (operand * operand).ToString();
            Answer.Value = model.Calculate(operand).ToString();
        }
    }
}
