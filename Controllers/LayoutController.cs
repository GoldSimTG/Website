﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Linq;
using System.Web.Mvc;
using Ignia.Topics;
using Ignia.Topics.Repositories;
using GoldSim.Web.Models;
using Ignia.Topics.Mapping;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: LAYOUT CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to the default homepage for the site.
  /// </summary>
  public class LayoutController : Controller {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository                = null;
    private readonly            ITopicRoutingService            _topicRoutingService            = null;
    private readonly            ITopicMappingService            _topicMappingService            = null;
    private                     Topic                           _currentTopic                   = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Topic Controller with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public LayoutController(
      ITopicRepository topicRepository,
      ITopicRoutingService topicRoutingService,
      ITopicMappingService topicMappingService
    ) : base() {
      _topicRepository          = topicRepository;
      _topicRoutingService      = topicRoutingService;
      _topicMappingService      = topicMappingService;
    }

    /*==========================================================================================================================
    | TOPIC REPOSITORY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the Topic Repository in order to gain arbitrary access to the entire topic graph.
    /// </summary>
    /// <returns>The TopicRepository associated with the controller.</returns>
    protected ITopicRepository TopicRepository {
      get {
        return _topicRepository;
      }
    }

    /*==========================================================================================================================
    | CURRENT TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the current topic associated with the request.
    /// </summary>
    /// <returns>The Topic associated with the current request.</returns>
    protected Topic CurrentTopic {
      get {
        if (_currentTopic == null) {
          _currentTopic = _topicRoutingService.GetCurrentTopic();
        }
        return _currentTopic;
      }
    }

    /*==========================================================================================================================
    | MENU
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the global menu for the site layout, which exposes the top two tiers of navigation.
    /// </summary>
    public PartialViewResult Menu() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var currentTopic          = CurrentTopic;
      var navigationRootTopic   = (Topic)null;

      /*------------------------------------------------------------------------------------------------------------------------
      | Identify navigation root
      >-------------------------------------------------------------------------------------------------------------------------
      | The navigation root in the case of the main menu is the namespace; i.e., the first topic underneath the root.
      \-----------------------------------------------------------------------------------------------------------------------*/
      navigationRootTopic = currentTopic;
      while (navigationRootTopic?.Parent?.Parent != null) {
        navigationRootTopic     = navigationRootTopic.Parent;
      }

      if (navigationRootTopic == null) {
        navigationRootTopic     = TopicRepository.Load("Web");
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Construct view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationViewModel   = AddNestedTopics(navigationRootTopic, currentTopic, false, 3);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the corresponding view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return PartialView(navigationViewModel);

    }

    /*==========================================================================================================================
    | PAGE LEVEL NAVIGATION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides page-level navigation for the current page.
    /// </summary>
    public PartialViewResult PageLevelNavigation() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationRootTopic   = (Topic)null;
      var currentTopic          = CurrentTopic;

      /*------------------------------------------------------------------------------------------------------------------------
      | Identify navigation root
      >-------------------------------------------------------------------------------------------------------------------------
      | The navigation root in the case of the page-level navigation any parent of content type "PageGroup".
      \-----------------------------------------------------------------------------------------------------------------------*/
      navigationRootTopic       = currentTopic;
      if (navigationRootTopic != null) {
        while (navigationRootTopic.Parent != null && !navigationRootTopic.ContentType.Equals("PageGroup")) {
          navigationRootTopic   = navigationRootTopic.Parent;
        }
      }

      if (navigationRootTopic?.Parent == null) navigationRootTopic = null;

      /*------------------------------------------------------------------------------------------------------------------------
      | Construct view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationViewModel   = AddNestedTopics(navigationRootTopic, currentTopic);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the corresponding view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return PartialView(navigationViewModel);

    }

    /*==========================================================================================================================
    | CALLS TO ACTION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the Calls To Action panel for the site layout.
    /// </summary>
    public PartialViewResult CallsToAction() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var currentTopic          = CurrentTopic;
      var navigationRootTopic   = (Topic)null;

      /*------------------------------------------------------------------------------------------------------------------------
      | Identify navigation root
      >-------------------------------------------------------------------------------------------------------------------------
      | The navigation root in the case of the main menu is the namespace; i.e., the first topic underneath the root.
      \-----------------------------------------------------------------------------------------------------------------------*/
      navigationRootTopic       = currentTopic;
      while (navigationRootTopic?.Parent?.Parent != null) {
        navigationRootTopic     = navigationRootTopic.Parent;
      }

      if (navigationRootTopic == null) {
        navigationRootTopic     = TopicRepository.Load("Web");
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Construct view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationViewModel   = AddNestedTopics(navigationRootTopic, currentTopic);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the corresponding view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return PartialView(navigationViewModel);

    }

    /*==========================================================================================================================
    | FOOTER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the footer for the site layout, which exposes the navigation from the company.
    /// </summary>
    public PartialViewResult Footer() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationRootTopic   = TopicRepository.Load("Web:Company");
      var currentTopic          = CurrentTopic;

      /*------------------------------------------------------------------------------------------------------------------------
      | Construct view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationViewModel   = AddNestedTopics(navigationRootTopic, currentTopic);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the corresponding view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return PartialView(navigationViewModel);

    }

    /*==========================================================================================================================
    | ADD NESTED TOPICS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   A helper function that allows a set number of tiers to be added to a <see cref="NavigationViewModel"/> tree.
    /// </summary>
    private NavigationViewModel AddNestedTopics(
      Topic sourceTopic,
      Topic currentTopic,
      bool allowPageGroups      = true,
      int tiers                 = 1
    ) {
      tiers--;
      if (sourceTopic == null) {
        return null;
      }
      var viewModel = _topicMappingService.Map<NavigationViewModel>(sourceTopic, Relationships.None);
      viewModel.IsSelected = (currentTopic?.GetUniqueKey()?? "").StartsWith(sourceTopic.GetUniqueKey());
      if (tiers >= 0 && (allowPageGroups || !sourceTopic.ContentType.Equals("PageGroup"))) {
        foreach (var topic in sourceTopic.Children.Sorted.Where(t => t.IsVisible())) {
          viewModel.Children.Add(
            AddNestedTopics(
              topic,
              currentTopic,
              allowPageGroups,
              tiers
            )
          );
        }
      }
      return viewModel;
    }

  } // Class
} // Namespace