using Prism.Events;
using Prism.Mvvm;
using Reactive.Bindings;
using Unity;
using PrismSample.Lib.Models;
using PrismSample.Lib.Views;


namespace PrismSample.Lib.ViewModels
{
    public class AnswerViewModel : BindableBase
    {
        public ReactiveProperty<string> Answer { get; }

        public ReactiveCommand<object> ShowDialogCommand { get; }
        public IModel model { get; set; }
       
        [Dependency]
        public IDialogHelper DialogHelper { get; set; }

        //public ReactiveCommand<object> anShowDialogCommand { get; }

        public AnswerViewModel(IEventAggregator eventAggregator)
        {
            Answer = new ReactiveProperty<string>("4");
            //should be constructed ???? or no need??
            model = new Model();

            eventAggregator
                .GetEvent<PubSubEvent<double>>()
                .Subscribe(CalculateAnswer);
            
            ShowDialogCommand = new ReactiveCommand()
                .WithSubscribe(_ => DialogHelper.ShowDialog($"N^2 = {Answer.Value}"));
        }

        private void CalculateAnswer(double operand)
        {
            //Answer.Value = (operand * operand).ToString();
            Answer.Value = model.Calculate(operand).ToString();
        }
    }
}
