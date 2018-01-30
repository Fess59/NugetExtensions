using FessooFramework.Tools.DCT;
using FessooFramework.Tools.Helpers;
using FessooFramework.Tools.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects
{
    /// <summary>   A system component multi.
    ///             Системный компонет поддерживающий множественную реализацию </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
    ///
    /// <typeparam name="TElement">     Type of the element. </typeparam>
    /// <typeparam name="TElementEnum"> Type of the element enum. </typeparam>

    public abstract class SystemComponentMulti<TElement, TElementEnum> : SystemComponent
        where TElement : _IOCElementEnum<TElementEnum>
        where TElementEnum : struct, IConvertible
    {
        #region Property
        /// <summary>
        /// Контейнер с реализациями 
        /// </summary>
        private static IOContainer<TElement> Container = new IOContainer<TElement>();
        #endregion
        #region Methods
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public TElement GetElement(TElementEnum itemType)
        {
            TElement result = default(TElement);
            DCTDefault.Execute(c => result = Container.GetByName(itemType.ToString()));
            return result;
        }
        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TElement> GetAll()
        {
            var result = Enumerable.Empty<TElement>();
            result = Container.GetAll();
            return result;
        }
        /// <summary>   Configurings this object. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        public override void _Configuring()
        {
            var list = new List<TElement>();
            _1_ElementAdd(ref list);
            foreach (var item in list)
            {
                var element = item;
                _2_ElementConfigurated(ref element);
                Container.Add(element);
            }
        }
        /// <summary>   Loadings this object. Загрузка данных для объекта и обработка данных. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        public override void _Loading()
        {
            _3_Loaded();
        }
        /// <summary>
        ///     Warnings this object. Проверка компонента на определённые условия - в будующем в этом
        ///     месте требуется подключение модуля динамической отладки с выводом предпреждений.
        /// </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        public override void _Warnings()
        {
            var cases = _4_CaseTesting();
            if (cases != null && cases.Any())
            {
                foreach (var c in cases)
                {
                    if (c.ComponentCase == null)
                    {
                        ConsoleHelper.SendWarning(MethodBase.GetCurrentMethod(), $"Case не возможно выполнить. Func не может быть NULL. Описание -  '{c.Description}' - тело вызова не может быть NULL");
                    }
                    else
                    {
                        try
                        {
                            if (!c.ComponentCase.Invoke())
                            {
                                ConsoleHelper.SendWarning(MethodBase.GetCurrentMethod(), $"Case не пройден! Описание - '{c.Description}'");
                            }
                        }
                        catch (Exception ex)
                        {
                            ConsoleHelper.SendWarning(MethodBase.GetCurrentMethod(), $"Case ошибка при выполнении! Описание - '{c.Description}'");
                        }

                    }
                }
            }
        }
        /// <summary>   Launchings this object. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        public override void _Launching()
        {
            _5_Run();
        }
        /// <summary>   Complitings this object. Работа с модулем была завершена. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        public override void _Compliting()
        {
            _99_Finalize();
        }
        #endregion
        #region Abstractions
        /// <summary>   Element add. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        ///
        /// <param name="elements"> [in,out] The elements. </param>
        public abstract void _1_ElementAdd(ref List<TElement> elements);
        /// <summary>   Element configurated. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        ///
        /// <param name="element">  [in,out] The element. </param>
        public abstract void _2_ElementConfigurated(ref TElement element);
        /// <summary>  Loaded this object. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        public abstract void _3_Loaded();
        /// <summary>   Case testing. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        ///
        /// <param name="cases">    [in,out] The cases. </param>
        public abstract IEnumerable<SystemComponentCase> _4_CaseTesting();
        /// <summary>   Runs this object. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        public abstract void _5_Run();
        /// <summary>   Finalize this object.
        ///             Вызывается перед Dispose - сохраняем необходимые параметры и фиксируем аналитические данные</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        public abstract void _99_Finalize();
        #endregion
    }

    public struct SystemComponentCase
    {
        public Func<bool> ComponentCase { get; set; }
        public string Description { get; set; }
        SystemComponentCase(string description, Func<bool> componentCase)
        {
            Description = description;
            ComponentCase = componentCase;
        }
        public static SystemComponentCase New(string description, Func<bool> componentCase)
        {
            return new SystemComponentCase()
            {
                Description = description,
                ComponentCase = componentCase
            };
        }
    }
}
