//-----------------------------------------------------------------------
//
//  Microsoft Windows Client Platform
//  Copyright (C) Microsoft Corporation, 2001
//
//  File:      TextEffect.cs
//
//  Contents:  TextEffect class
//
//  Created:   3/23/2004 garyyang
//
//------------------------------------------------------------------------


using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Markup;

namespace System.Windows.Media
{
    /// <summary>
    /// Collection of TextEffect
    /// </summary>
    [Localizability(LocalizationCategory.None, Readability=Readability.Unreadable)]
    public sealed partial class TextEffectCollection : Animatable, IList, IList<TextEffect>
    { 
    }
}
