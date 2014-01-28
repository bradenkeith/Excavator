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
using System.Web.UI;

using Rock.PersonProfile;

namespace Rock.Web.UI.Controls
{
    /// <summary>
    /// abstract class for controls used to render a Person Profile Badge
    /// </summary>
    public class PersonProfileBadge : Control
    {
        /// <summary>
        /// Gets or sets the name of the badge entity type.
        /// </summary>
        /// <value>
        /// The name of the badge entity type.
        /// </value>
        public string BadgeEntityTypeName
        {
            get { return ViewState["BadgeEntityName"] as string; }
            set { ViewState["BadgeEntityName"] = value; }
        }

        /// <summary>
        /// Gets the parent person block.
        /// </summary>
        /// <value>
        /// The parent person block.
        /// </value>
        public PersonBlock ParentPersonBlock
        {
            get
            {
                var parentControl = this.Parent;

                while ( parentControl != null )
                {
                    if ( parentControl is PersonBlock )
                    {
                        return parentControl as PersonBlock;
                    }

                    parentControl = parentControl.Parent;
                }

                return null;
            }

        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit( System.EventArgs e )
        {
            base.OnInit( e );
        }

        /// <summary>
        /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the server control content.</param>
        protected override void Render( HtmlTextWriter writer )
        {
            if ( !string.IsNullOrWhiteSpace( BadgeEntityTypeName ) )
            {
                var badgeComponent = BadgeContainer.GetComponent( BadgeEntityTypeName );
                if ( badgeComponent != null )
                {
                    var personBlock = ParentPersonBlock;
                    if ( personBlock != null )
                    {
                        badgeComponent.ParentPersonBlock = personBlock;
                        badgeComponent.Person = personBlock.Person;
                        badgeComponent.Render( writer );
                    }
                }
            }

            string script = "$('.badge').tooltip({html: true});";
            ScriptManager.RegisterStartupScript( this, this.GetType(), "badge-popover", script, true );

        }
    }
}