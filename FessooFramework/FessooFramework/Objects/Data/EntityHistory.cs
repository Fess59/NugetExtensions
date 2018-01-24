using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.Data
{
    /// <summary>   An entity change history model. </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 24.01.2018. </remarks>

    public class EntityHistory : DataObject
    {
        /// <summary>   Gets or sets the identifier of the modified object. </summary>
        ///
        /// <value> The identifier of the modified object. </value>

        public Guid ObjectId { get; set; }

        /// <summary>   Gets or sets the type of the modified object. </summary>
        ///
        /// <value> The type of the modified object. </value>

        public string ObjectType { get; set; }

        /// <summary>   Gets or sets the identifier of the user who owns the change. </summary>
        ///
        /// <value> The identifier of the user who owns the change. </value>

        public Guid? OwnerId { get; set; }

        /// <summary>   Gets or sets the comment. </summary>
        ///
        /// <value> The comment. </value>

        public string Comment { get; set; }

        /// <summary>   Creates a new model. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 11.01.2018. </remarks>
        ///
        /// <exception cref="NullReferenceException">   Thrown when a value was unexpectedly null. ObjectId cannot Guid.Empty</exception>
        ///
        /// <param name="objectId">     The identifier of the modified object. </param>
        /// <param name="objectType">   The type of the modified object. </param>
        /// <param name="userId">       The identifier of the user who owns the change. </param>
        /// <param name="comment">      The comment. </param>
        ///
        /// <returns>   A ChangeHistory. </returns>

        public static EntityHistory New(Guid objectId, string objectType, Guid? ownerId, string comment)
        {
            if (objectId == Guid.Empty)
                throw new NullReferenceException("EntityHistory.New ObjectId cannot Guid.Empty");
            return new EntityHistory()
            {
                ObjectId = objectId,
                ObjectType = objectType,
                OwnerId = ownerId,
                Comment = comment
            };
        }
    }
}
