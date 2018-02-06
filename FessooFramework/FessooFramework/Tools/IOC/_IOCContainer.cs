using FessooFramework.Objects;
using FessooFramework.Tools.DCT;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.IOC
{
    /// <summary>   An i/o container. </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 05.02.2018. </remarks>
    ///
    /// <typeparam name="T">    Generic type parameter. </typeparam>

    public class IOContainer<T> : SystemObject where T : _IOCElement
    {
        #region Property

        /// <summary>   The collection of IOC elements. </summary>
        private List<T> Collection = new List<T>();

        /// <summary>   Gets or sets the name of the container. </summary>
        ///
        /// <value> The name of the container. </value>

        public string ContainerName { get; set; }
        private bool HasElementCreate { get; set; }
        #endregion
        #region Constructor

        /// <summary>   Default constructor of IOC container. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 11.01.2018. </remarks>

        public IOContainer(bool hasElementCreate = false)
        {
            HasElementCreate = hasElementCreate;
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
                ConsoleHelper.SendException(ex);
            }
        }
        #endregion
        #region Methods

        /// <summary>   Gets <see cref="T" /> by UID. Аргумент autoCreate отвечает за создание элемента если он не был найден</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 11.01.2018. </remarks>
        ///
        /// <param name="uid">  The UID of IOC element. </param>
        /// <param name="autoCreate"> If True - UID not found enable, else False - UID not found disable and excute Create method). </param>
        ///
        /// <returns>   The by name. </returns>

        public T GetByName(string uid)
        {
            T result = default(T);
            DCTDefault.Execute(c =>
            {
                if (HasElementCreate)
                {
                    result = Collection.FirstOrDefault(q => q.UID == uid);
                    if (result == null)
                        result = Create(uid);
                }
                else
                {
                    if (Collection == null || !Collection.Any())
                        throw new Exception($"IOC container empty. IOC element with the specified UID not found => {this.GetType().Name}.Add({uid})");
                    result = Collection.FirstOrDefault(q => q.UID == uid);
                    if (result == null)
                        throw new Exception($"IOC element with the specified UID not found => {this.GetType().Name}.Add({uid})");
                }
            });
            return result;
        }

        /// <summary>   Gets all items in this collection.
        ///             Получаем все объекты для обработки </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>

        public IEnumerable<T> GetAll()
        {
            return Collection.ToArray();
        }

        /// <summary>   Adds IOC element in container. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 11.01.2018. </remarks>
        ///
        /// <param name="element">  The element to add. </param>

        public T Add(T element)
        {
            var result = default(T);
            DCTDefault.Execute(c =>
            {
                if (Collection.Any(q => q.UID == element.UID))
                    throw new Exception($"IOC element with the specified UID has already been added => {this.GetType().Name}.Add({element.UID})");
                //throw new ExceptionFlowIOContainer("AddRange", "IOC element with the specified UID has already been added");
                Collection.Add(element);
                result = element;
            });
            return result;
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
            foreach (var element in collection)
                Add(element);
        }

        /// <summary>   Creates a new T.
        ///             Создание нового элемента для контейнера если он отсутсвует в контейнере </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 05.02.2018. </remarks>
        ///
        /// <param name="uid">  The UID of IOC element. </param>
        ///
        /// <returns>   A T. </returns>

        public virtual T Create(string uid)
        {
            throw new NotImplementedException($"IOC {this.GetType().Name} not implemented methods Create, please override Create or disable argument 'HasElementCreate' from container base constructor");
        }

        /// <summary>   Enumerates where in this collection. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 05.02.2018. </remarks>
        ///
        /// <param name="predicate">    The predicate. </param>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process where in this collection.
        /// </returns>

        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return Collection.AsQueryable().Where(predicate).ToArray();
        }
    }
    #endregion
}
