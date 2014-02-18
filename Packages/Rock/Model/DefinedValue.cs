﻿// <copyright>
// Copyright 2013 by the Spark Development Network
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;

using Rock.Data;

namespace Rock.Model
{
    /// <summary>
    /// Represents a DefinedValue instance in Rock. These values are sortable and can be secured (based on their <see cref="ParentAuthority"/>). 
    /// An example of a DefinedValue for a "State List" <see cref="Rock.Model.DefinedType"/> is Arizona.
    /// </summary>
    [Table( "DefinedValue" )]
    [DataContract]
    public partial class DefinedValue : Model<DefinedValue>, IOrdered
    {
        /// <summary>
        /// Gets or sets a flag indicating if this DefinedValue is part of the Rock core system/framework. this property is required.
        /// </summary>
        /// <value>
        /// A <see cref="System.Boolean"/> that is <c>true</c> if it is part of the Rock core system/framework; otherwise <c>false</c>.
        /// </value>
        [Required]
        [DataMember( IsRequired = true )]
        public bool IsSystem { get; set; }

        /// <summary>
        /// Gets or sets the DefinedTypeId of the <see cref="Rock.Model.DefinedType"/> that this DefinedValue belongs to. This property is required.
        /// </summary>
        /// <value>
        /// A <see cref="System.Int32"/> representing the DefinedTypeId of the <see cref="Rock.Model.DefinedType"/> that this DefinedValue belongs to.
        /// </value>
        [Required]
        [DataMember( IsRequired = true )]
        public int DefinedTypeId { get; set; }

        /// <summary>
        /// Gets or sets the sort and display order of the DefinedValue.  This is an ascending order, so the lower the value the higher the sort priority.
        /// </summary>
        /// <value>
        /// A <see cref="System.Int32"/> representing the sort order of the DefinedValue.
        /// </value>
        [Required]
        [DataMember( IsRequired = true )]
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the Name or the value of the DefinedValue. This property is required.
        /// </summary>
        /// <value>
        /// A <see cref="System.String"/> that represents the Name or the value of the DefinedValue.
        /// </value>
        [Required]
        [MaxLength( 100 )]
        [DataMember( IsRequired = true )]
        [MergeField]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description of the DefinedValue.
        /// </summary>
        /// <value>
        /// A <see cref="System.String"/> representing the description of the DefinedValue.
        /// </value>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Defined Type that this DefinedValue belongs to.
        /// </summary>
        /// <value>
        /// The <see cref="Rock.Model.DefinedType"/> that this DefinedValue belongs to.
        /// </value>
        [DataMember]
        public virtual DefinedType DefinedType { get; set; }

        /// <summary>
        /// Gets the parent security authority for this DefinedValue.
        /// </summary>
        /// <value>
        /// An entity that implements the <see cref="Rock.Security.ISecured"/> interface that this DefinedValue inherits security authority from.
        /// </value>
        public override Security.ISecured ParentAuthority
        {
            get { return this.DefinedType; }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this DefinedValue.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this DefinedValue.
        /// </returns>
        public override string ToString()
        {
            return this.Name;
        }
    }

    /// <summary>
    /// Defined Value Configuration class.
    /// </summary>
    public partial class DefinedValueConfiguration : EntityTypeConfiguration<DefinedValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefinedValueConfiguration"/> class.
        /// </summary>
        public DefinedValueConfiguration()
        {
            this.HasRequired( p => p.DefinedType ).WithMany( p => p.DefinedValues ).HasForeignKey( p => p.DefinedTypeId ).WillCascadeOnDelete( true );
        }
    }
}
