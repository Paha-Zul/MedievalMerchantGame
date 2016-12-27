namespace Util
{
    [System.Serializable]
    public class MutablePair<T, R>  {
        public T first;
        public R second;

        public MutablePair(T first, R second) {
            this.first = first;
            this.second = second;
        }
	
    }
}
