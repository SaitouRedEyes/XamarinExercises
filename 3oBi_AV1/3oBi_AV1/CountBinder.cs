using Android.OS;

namespace _3oBi_AV1
{
    class CountBinder : Binder
    {
        public CountBinder(CountService service)
        {
            Service = service;
        }

        public CountService Service
        {
            get; private set;
        }
    }
}