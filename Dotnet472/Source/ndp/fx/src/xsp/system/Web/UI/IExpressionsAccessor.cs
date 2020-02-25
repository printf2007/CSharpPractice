//------------------------------------------------------------------------------
// <copyright file="IExpressionsAccessor.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace System.Web.UI {
    using System.Collections;

    public interface IExpressionsAccessor {


        bool HasExpressions {
            get;
        }


        ExpressionBindingCollection Expressions {
            get;
        }
    }
}
