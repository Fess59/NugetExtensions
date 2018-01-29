using FessooFramework.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.IOC
{
    /// <summary>   An i/o container. </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 11.01.2018. </remarks>
    ///
    /// <typeparam name="T">    Generic type parameter. Where T is  <see cref="IOCElement"/> </typeparam>

    public class IOContainer<T> : SystemObject where T : IOCElement
    {
        #region Property

        /// <summary>   The collection of IOC elements. </summary>
        private List<T> Collection = new List<T>();

        /// <summary>   Gets or sets the name of the container. </summary>
        ///
        /// <value> The name of the container. </value>

        public string ContainerName { get; set; }
        #endregion
        #region Constructor

        /// <summary>   Default constructor of IOC container. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 11.01.2018. </remarks>

        public IOContainer()
        {

        }

        /// <summary>   Constructor of IOC container with the help of elements list. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 11.01.2018. </remarks>
        ///
        /// <param name="elements"> The elements of IOC container - <see cref="T" /> . </param>

        public IOContainer(IEnumerable<T> elements)
        {
            try
            {
                AddRange(elements);
            }
            catch (Exception ex)
            {
                Logger.SendMessage(ex.ToString());
            }
        }
        #endregion
        #region Methods

        /// <summary>   Gets <see cref="T" /> by UID. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 11.01.2018. </remarks>
        ///
        /// <param name="uid">  The UID of IOC element. </param>
        ///
        /// <returns>   The by name. </returns>

        public T GetByName(string uid)
        {
            T result = default(T);
            if (Collection == null && !Collection.Any())
                return result;
            result = Collection.FirstOrDefault(q => q.UID == uid);
            return result;
        }

        /// <summary>   Adds IOC element in container. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 11.01.2018. </remarks>
        ///
        /// <param name="element">  The element to add. </param>

        public void Add(T element)
        {
            AddRange(new T[] { element });
        }

        /// <summary>   Adds IOC element collection in container. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 11.01.2018. </remarks>
        ///
        /// <exception cref="ExceptionFlowIOContainer"> Thrown when an IOC element with the specified UID has already been added. </exception>
        ///
        /// <param name="collection">   The collection of IOC elements. </param>

        public void AddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                if (Collection.Any(q => q.UID == item.UID))
                    throw new ExceptionFlowIOContainer("AddRange", "IOC element with the specified UID has already been added");
                Collection.Add(item);
            }
        }
        #endregion
    }
}
