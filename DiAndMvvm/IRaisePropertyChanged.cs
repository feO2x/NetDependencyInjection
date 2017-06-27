namespace DiAndMvvm
{
    public interface IRaisePropertyChanged
    {
        void OnPropertyChanged(string propertyName = null);
    }
}