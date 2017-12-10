using System.Collections.Generic;

namespace CitySearch
{
    public interface ICityFinder
    {
        /*
         Note that this population wasn't in the provided interface but it is necessary for the interface 
         to be aware of a data store so a case of either referencing a data store or creating within the finder.
         As the interface defines the search method I am preferring to create the store within this class than provide
         some sort of delegate search method
         */
        void Populate(ICollection<string> Cities);
        ICityResult Search(string searchString);
    }
}
