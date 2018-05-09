﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.Collections.ObjectModel;
using System.ComponentModel;
using Ignia.Topics;
using Ignia.Topics.ViewModels;

namespace GoldSim.Web.Models {

  /*============================================================================================================================
  | VIEW MODEL: NAVIGATION TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about the navigation.
  /// </summary>
  /// <remarks>
  ///   No topics are expected to have a <c>Navigation</c> content type. Instead, this view model is expected to be manually
  ///   constructed by the <see cref="LayoutController"/>.
  /// </remarks>
  public class NavigationTopicViewModel: PageTopicViewModel, INavigationTopicViewModelCore<NavigationTopicViewModel> {

    public string HeaderImageUrl { get; set; }
    public virtual Collection<NavigationTopicViewModel> Children { get; set; }
    public bool IsSelected(string uniqueKey) => uniqueKey?.StartsWith(UniqueKey) ?? false;

  } // Class

} // Namespace