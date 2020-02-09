using System;
using System.Reflection;
using System.Globalization;
using System.Resources;
using System.Text;
using System.ComponentModel;
using System.Workflow.Activities;
using System.Drawing;
using System.Workflow.ComponentModel.Design;

[AttributeUsage(AttributeTargets.All)]
internal sealed class SRDescriptionAttribute : DescriptionAttribute
{
    public SRDescriptionAttribute(string description)
    {
        DescriptionValue = SR.GetString(description);
    }

    public SRDescriptionAttribute(string description, string resourceSet)
    {
        ResourceManager rm = new ResourceManager(resourceSet, Assembly.GetExecutingAssembly());
        DescriptionValue = rm.GetString(description);
        System.Diagnostics.Debug.Assert(DescriptionValue != null, string.Format(CultureInfo.CurrentCulture, "String resource {0} not found.", description));
    }
}

[AttributeUsage(AttributeTargets.All)]
internal sealed class SRCategoryAttribute : CategoryAttribute
{
    private string resourceSet = String.Empty;

    public SRCategoryAttribute(string category)
        : base(category)
    {
    }

    public SRCategoryAttribute(string category, string resourceSet)
        : base(category)
    {
        this.resourceSet = resourceSet;
    }

    protected override string GetLocalizedString(string value)
    {
        if (this.resourceSet.Length > 0)
        {
            ResourceManager rm = new ResourceManager(resourceSet, Assembly.GetExecutingAssembly());
            String localizedString = rm.GetString(value);
            System.Diagnostics.Debug.Assert(localizedString != null, string.Format(CultureInfo.CurrentCulture, "String resource {0} not found.", value));
            return localizedString;
        }
        else
        {
            return SR.GetString(value);
        }
    }
}

[AttributeUsage(AttributeTargets.All)]
internal sealed class SRDisplayNameAttribute : DisplayNameAttribute
{
    public SRDisplayNameAttribute(string name)
    {
        DisplayNameValue = SR.GetString(name);
    }

    public SRDisplayNameAttribute(string name, string resourceSet)
    {
        ResourceManager rm = new ResourceManager(resourceSet, Assembly.GetExecutingAssembly());
        DisplayNameValue = rm.GetString(name);
        System.Diagnostics.Debug.Assert(DisplayNameValue != null, string.Format(CultureInfo.CurrentCulture, "String resource {0} not found.", name));
    }
}

/// <summary>
///    AutoGenerated resource class. Usage:
///
///        string s = SR.GetString(SR.MyIdenfitier);
/// </summary>
internal sealed class SR
{
    static SR loader = null;
    ResourceManager resources;

    internal SR()
    {
        resources = new System.Resources.ResourceManager("System.Workflow.Activities.StringResources", Assembly.GetExecutingAssembly());
    }

    private static SR GetLoader()
    {
        if (loader == null)
            loader = new SR();
        return loader;
    }

    private static CultureInfo Culture
    {
        get { return null/*use ResourceManager default, CultureInfo.CurrentUICulture*/; }
    }

    internal static string GetString(string name, params object[] args)
    {
        return GetString(SR.Culture, name, args);
    }
    internal static string GetString(CultureInfo culture, string name, params object[] args)
    {
        SR sys = GetLoader();
        if (sys == null)
            return null;
        string res = sys.resources.GetString(name, culture);
        System.Diagnostics.Debug.Assert(res != null, string.Format(CultureInfo.CurrentCulture, "String resource {0} not found.", name));
        if (args != null && args.Length > 0)
        {
            return String.Format(CultureInfo.CurrentCulture, res, args);
        }
        else
        {
            return res;
        }
    }

    internal static string GetString(string name)
    {
        return GetString(SR.Culture, name);
    }

    internal static string GetString(CultureInfo culture, string name)
    {
        SR sys = GetLoader();
        if (sys == null)
            return null;
        string res = sys.resources.GetString(name, culture);
        System.Diagnostics.Debug.Assert(res != null, string.Format(CultureInfo.CurrentCulture, "String resource {0} not found.", name));
        return res;
    }

    // All these strings should be present in StringResources.resx
    internal const string Activity = "Activity";
    internal const string Handlers = "Handlers";
    internal const string Conditions = "Conditions";
    internal const string ConditionedActivityConditions = "ConditionedActivityConditions";
    internal const string CorrelationSet = "CorrelationSet";
    internal const string NameDescr = "NameDescr";
    internal const string UserCodeHandlerDescr = "UserCodeHandlerDescr";
    internal const string ExpressionDescr = "ExpressionDescr";
    internal const string ExecutionTypeDescr = "ExecutionTypeDescr";
    internal const string InitialChildDataDescr = "InitialChildDataDescr";
    internal const string ConditionDescr = "ConditionDescr";
    internal const string UntilConditionDescr = "UntilConditionDescr";
    internal const string WhenConditionDescr = "WhenConditionDescr";
    internal const string TargetWorkflowDescr = "TargetWorkflowDescr";
    internal const string InitializeCaleeDescr = "InitializeCaleeDescr";
    internal const string ProxyClassDescr = "ProxyClassDescr";
    internal const string MethodNameDescr = "MethodNameDescr";
    internal const string URLDescr = "URLDescr";
    internal const string ActivationDescr = "ActivationDescr";
    internal const string OnAfterMethodInvokeDescr = "OnAfterMethodInvokeDescr";
    internal const string OnBeforeMethodInvokeDescr = "OnBeforeMethodInvokeDescr";
    internal const string TypeDescr = "TypeDescr";
    internal const string WhileConditionDescr = "WhileConditionDescr";
    internal const string ReplicatorUntilConditionDescr = "ReplicatorUntilConditionDescr";
    internal const string DynamicUpdateConditionDescr = "DynamicUpdateConditionDescr";
    internal const string CorrelationSetDescr = "CorrelationSetDescr";
    internal const string RoleDescr = "RoleDescr";
    internal const string ChangingVariable = "ChangingVariable";
    internal const string OnInitializedDescr = "OnInitializedDescr";
    internal const string OnCompletedDescr = "OnCompletedDescr";
    internal const string Type = "Type";
    internal const string InterfaceTypeDescription = "InterfaceTypeDescription";
    internal const string InterfaceTypeFilterDescription = "InterfaceTypeFilterDescription";
    internal const string WebServiceMethodDescription = "WebServiceMethodDescription";
    internal const string ReceiveActivityNameDescription = "ReceiveActivityNameDescription";
    internal const string WebServiceSessionIDDescr = "WebServiceSessionIDDescr";
    internal const string OnAfterReceiveDescr = "OnAfterReceiveDescr";
    internal const string OnBeforeResponseDescr = "OnBeforeResponseDescr";
    internal const string OnBeforeFaultingDescr = "OnBeforeFaultingDescr";
    internal const string TimeoutDurationDescription = "TimeoutDurationDescription";
    internal const string TimeoutInitializerDescription = "TimeoutInitializerDescription";
    internal const string StateMachineWorkflow = "StateMachineWorkflow";
    internal const string SequentialWorkflow = "SequentialWorkflow";
    internal const string EventSink = "EventSink";
    internal const string RuleSetDescription = "RuleSetDescription";
    internal const string RuleSetDefinitionDescription = "RuleSetDefinitionDescription";
    internal const string ConnectorColorDescription = "ConnectorColorDescription";
    internal const string InitialStateImagePathDescription = "InitialStateImagePathDescription";
    internal const string CompletedStateImagePathDescription = "CompletedStateImagePathDescription";

    internal const string Error_ConditionalBranchParentNotConditional = "Error_ConditionalBranchParentNotConditional";
    internal const string Error_EventDrivenMultipleEventActivity = "Error_EventDrivenMultipleEventActivity";
    internal const string Error_ParameterPropertyNotSet = "Error_ParameterPropertyNotSet";
    internal const string Error_ListenNotAllEventDriven = "Error_ListenNotAllEventDriven";
    internal const string Error_InterfaceTypeNotInterface = "Error_InterfaceTypeNotInterface";
    internal const string Error_ParallelLessThanTwoChildren = "Error_ParallelLessThanTwoChildren";
    internal const string Error_PropertyNotSet = "Error_PropertyNotSet";
    internal const string Error_MissingCorrelationParameterAttribute = "Error_MissingCorrelationParameterAttribute";
    internal const string Error_MissingCorrelationTokenProperty = "Error_MissingCorrelationTokenProperty";
    internal const string Error_CorrelationTypeNotConsistent = "Error_CorrelationTypeNotConsistent";
    internal const string Error_CorrelationInvalid = "Error_CorrelationInvalid";
    internal const string Error_MissingMethodName = "Error_MissingMethodName";
    internal const string Error_MissingEventName = "Error_MissingEventName";
    internal const string Error_ListenLessThanTwoChildren = "Error_ListenLessThanTwoChildren";
    internal const string Error_MethodNotExists = "Error_MethodNotExists";
    internal const string General_MissingService = "General_MissingService";
    internal const string Error_FieldNotExists = "Error_FieldNotExists";
    internal const string Error_TypeNotResolved = "Error_TypeNotResolved";
    internal const string Error_ParameterNotFound = "Error_ParameterNotFound";
    internal const string Error_TypeNotExist = "Error_TypeNotExist";
    internal const string Error_ParallelNotAllSequence = "Error_ParallelNotAllSequence";
    internal const string Error_ActivationActivityNotFirst = "Error_ActivationActivityNotFirst";
    internal const string Error_ActivationActivityInsideLoop = "Error_ActivationActivityInsideLoop";
    internal const string Error_DuplicateCorrelation = "Error_DuplicateCorrelation";
    internal const string Error_NegativeValue = "Error_NegativeValue";
    internal const string Error_MustHaveParent = "Error_MustHaveParent";
    internal const string Error_CanNotChangeAtRuntime = "Error_CanNotChangeAtRuntime";
    internal const string Error_CannotNestThisActivity = "Error_CannotNestThisActivity";
    internal const string Error_GetCalleeWorkflow = "Error_GetCalleeWorkflow";
    internal const string Error_TypeIsNotRootActivity = "Error_TypeIsNotRootActivity";
    internal const string Error_ContextStackItemMissing = "Error_ContextStackItemMissing";
    internal const string Error_UnexpectedArgumentType = "Error_UnexpectedArgumentType";
    internal const string OnGeneratorChildCompletedDescr = "OnGeneratorChildCompletedDescr";
    internal const string OnGeneratorChildInitializedDescr = "OnGeneratorChildInitializedDescr";
    internal const string Error_WebServiceResponseNotFound = "Error_WebServiceResponseNotFound";
    internal const string Error_WebServiceReceiveNotFound = "Error_WebServiceReceiveNotFound";
    internal const string Error_WebServiceResponseNotNeeded = "Error_WebServiceResponseNotNeeded";
    internal const string Error_WebServiceFaultNotNeeded = "Error_WebServiceFaultNotNeeded";
    internal const string Error_WebServiceReceiveNotConfigured = "Error_WebServiceReceiveNotConfigured";
    internal const string Error_WebServiceReceiveNotMarkedActivate = "Error_WebServiceReceiveNotMarkedActivate";
    internal const string Error_DuplicateWebServiceResponseFound = "Error_DuplicateWebServiceResponseFound";
    internal const string Error_DuplicateWebServiceFaultFound = "Error_DuplicateWebServiceFaultFound";
    internal const string Error_CAGChildNotFound = "Error_CAGChildNotFound";
    internal const string Error_CAGNotExecuting = "Error_CAGNotExecuting";
    internal const string Error_CAGQuiet = "Error_CAGQuiet";
    internal const string Error_CAGDynamicUpdateNotAllowed = "Error_CAGDynamicUpdateNotAllowed";
    internal const string Error_MissingValidationProperty = "Error_MissingValidationProperty";
    internal const string Error_MissingConditionName = "Error_MissingConditionName";
    internal const string Error_MissingRuleConditions = "Error_MissingRuleConditions";
    internal const string Error_RoleProviderNotAvailableOrEnabled = "Error_RoleProviderNotAvailableOrEnabled";
    internal const string Error_ExternalDataExchangeServiceExists = "Error_ExternalDataExchangeServiceExists";
    internal const string Error_WorkflowTerminated = "Error_WorkflowTerminated";
    internal const string Error_WorkflowCompleted = "Error_WorkflowCompleted";
    internal const string Warning_AdditionalBindingsFound = "Warning_AdditionalBindingsFound";
    internal const string Error_ConfigurationSectionNotFound = "Error_ConfigurationSectionNotFound";
    internal const string Error_UnknownConfigurationParameter = "Error_UnknownConfigurationParameter";

    internal const string Error_CannotConnectToRequest = "Error_CannotConnectToRequest";

    // state machine errors
    internal const string Error_StateChildNotFound = "Error_StateChildNotFound";

    private const string Error_InvalidStateActivityParent = "Error_InvalidStateActivityParent";
    internal static string GetError_InvalidStateActivityParent()
    {
        return GetString(Error_InvalidStateActivityParent,
            typeof(StateActivity).Name);
    }

    private const string Error_BlackBoxCustomStateNotSupported = "Error_BlackBoxCustomStateNotSupported";
    internal static string GetError_BlackBoxCustomStateNotSupported()
    {
        return GetString(Error_BlackBoxCustomStateNotSupported,
            typeof(StateActivity).Name);
    }

    private const string Error_InvalidLeafStateChild = "Error_InvalidLeafStateChild";
    internal static string GetError_InvalidLeafStateChild()
    {
        return GetString(Error_InvalidLeafStateChild,
            typeof(StateActivity).Name,
            typeof(EventDrivenActivity).Name,
            typeof(StateInitializationActivity).Name,
            typeof(StateFinalizationActivity).Name);
    }

    private const string Error_InvalidCompositeStateChild = "Error_InvalidCompositeStateChild";
    internal static string GetError_InvalidCompositeStateChild()
    {
        return GetString(Error_InvalidCompositeStateChild,
            typeof(StateMachineWorkflowActivity).Name,
            typeof(StateActivity).Name,
            typeof(EventDrivenActivity).Name);
    }

    private const string Error_SetStateOnlyWorksOnStateMachineWorkflow = "Error_SetStateOnlyWorksOnStateMachineWorkflow";
    internal static string GetError_SetStateOnlyWorksOnStateMachineWorkflow()
    {
        return GetString(Error_SetStateOnlyWorksOnStateMachineWorkflow,
            typeof(SetStateActivity).Name,
            typeof(EventDrivenActivity).Name,
            typeof(StateInitializationActivity).Name,
            typeof(StateMachineWorkflowActivity).Name,
            typeof(StateActivity).Name);
    }

    private const string Error_SetStateMustPointToAState = "Error_SetStateMustPointToAState";
    internal static string GetError_SetStateMustPointToAState()
    {
        return GetString(Error_SetStateMustPointToAState,
            SetStateActivity.TargetStateNameProperty,
            typeof(StateActivity).Name);
    }

    private const string Error_StateActivityMustBeContainedInAStateMachine = "Error_StateActivityMustBeContainedInAStateMachine";
    internal static string GetError_StateActivityMustBeContainedInAStateMachine()
    {
        return GetString(Error_StateActivityMustBeContainedInAStateMachine,
            typeof(StateActivity).Name,
            typeof(StateMachineWorkflowActivity).Name,
            StateMachineWorkflowActivity.InitialStateNamePropertyName);
    }

    private const string Error_CannotExecuteStateMachineWithoutInitialState = "Error_CannotExecuteStateMachineWithoutInitialState";
    internal static string GetError_CannotExecuteStateMachineWithoutInitialState()
    {
        return GetString(Error_CannotExecuteStateMachineWithoutInitialState,
            typeof(StateMachineWorkflowActivity).Name,
            StateMachineWorkflowActivity.InitialStateNamePropertyName);
    }

    internal static string GetError_InitialStateMustPointToAState()
    {
        return GetString(Error_SetStateMustPointToAState,
            StateMachineWorkflowActivity.InitialStateNamePropertyName,
            typeof(StateActivity).Name);
    }

    internal static string GetError_CompletedStateMustPointToAState()
    {
        return GetString(Error_SetStateMustPointToAState,
            StateMachineWorkflowActivity.CompletedStateNamePropertyName,
            typeof(StateActivity).Name);
    }

    private const string Error_SetStateMustPointToALeafNodeState = "Error_SetStateMustPointToALeafNodeState";
    internal static string GetError_SetStateMustPointToALeafNodeState()
    {
        return GetString(Error_SetStateMustPointToALeafNodeState,
            SetStateActivity.TargetStateNameProperty,
            typeof(StateActivity).Name);
    }

    internal static string GetError_InitialStateMustPointToALeafNodeState()
    {
        return GetString(Error_SetStateMustPointToALeafNodeState,
            StateMachineWorkflowActivity.InitialStateNameProperty,
            typeof(StateActivity).Name);
    }

    internal static string GetError_CompletedStateMustPointToALeafNodeState()
    {
        return GetString(Error_SetStateMustPointToALeafNodeState,
            StateMachineWorkflowActivity.CompletedStateNamePropertyName,
            typeof(StateActivity).Name);
    }

    private const string Error_InitialStateMustBeDifferentThanCompletedState = "Error_InitialStateMustBeDifferentThanCompletedState";
    internal static string GetError_InitialStateMustBeDifferentThanCompletedState()
    {
        return GetString(Error_InitialStateMustBeDifferentThanCompletedState,
            StateMachineWorkflowActivity.InitialStateNameProperty,
            StateMachineWorkflowActivity.CompletedStateNameProperty);
    }

    internal const string Error_CompletedStateCannotContainActivities = "Error_CompletedStateCannotContainActivities";

    private const string Error_StateHandlerParentNotState = "Error_StateHandlerParentNotState";
    internal static string GetError_StateInitializationParentNotState()
    {
        return GetString(Error_StateHandlerParentNotState,
            typeof(StateInitializationActivity).Name,
            typeof(StateActivity).Name);
    }

    internal static string GetError_StateFinalizationParentNotState()
    {
        return GetString(Error_StateHandlerParentNotState,
            typeof(StateFinalizationActivity).Name,
            typeof(StateActivity).Name);
    }

    private const string Error_EventActivityNotValidInStateHandler = "Error_EventActivityNotValidInStateHandler";
    internal static string GetError_EventActivityNotValidInStateInitialization()
    {
        return GetString(Error_EventActivityNotValidInStateHandler,
            typeof(StateInitializationActivity).Name,
            typeof(IEventActivity).FullName);
    }

    internal static string GetError_EventActivityNotValidInStateFinalization()
    {
        return GetString(Error_EventActivityNotValidInStateHandler,
            typeof(StateFinalizationActivity).Name,
            typeof(IEventActivity).FullName);
    }

    private const string Error_MultipleStateHandlerActivities = "Error_MultipleStateHandlerActivities";
    internal static string GetError_MultipleStateInitializationActivities()
    {
        return GetString(Error_MultipleStateHandlerActivities,
            typeof(StateInitializationActivity).Name,
            typeof(StateActivity).Name);
    }

    internal static string GetError_MultipleStateFinalizationActivities()
    {
        return GetString(Error_MultipleStateHandlerActivities,
            typeof(StateFinalizationActivity).Name,
            typeof(StateActivity).Name);
    }

    private const string Error_InvalidTargetStateInStateInitialization = "Error_InvalidTargetStateInStateInitialization";
    internal static string GetError_InvalidTargetStateInStateInitialization()
    {
        return GetString(Error_InvalidTargetStateInStateInitialization,
            typeof(SetStateActivity).Name,
            SetStateActivity.TargetStateNamePropertyName,
            typeof(StateActivity).Name,
            typeof(StateInitializationActivity).Name);
    }

    private const string Error_CantRemoveState = "Error_CantRemoveState";
    internal static string GetError_CantRemoveState(string stateName)
    {
        return GetString(Error_CantRemoveState,
            typeof(StateActivity).Name,
            stateName);
    }

    private const string Error_CantRemoveEventDrivenFromExecutingState = "Error_CantRemoveEventDrivenFromExecutingState";
    internal static string GetError_CantRemoveEventDrivenFromExecutingState(string eventDrivenName, string parentStateName)
    {
        return GetString(Error_CantRemoveEventDrivenFromExecutingState,
            typeof(EventDrivenActivity).Name,
            eventDrivenName,
            typeof(StateActivity).Name,
            parentStateName);
    }

    private const string SqlTrackingServiceRequired = "SqlTrackingServiceRequired";
    internal static string GetSqlTrackingServiceRequired()
    {
        return GetString(SqlTrackingServiceRequired,
            StateMachineWorkflowInstance.StateHistoryPropertyName,
            typeof(System.Workflow.Runtime.Tracking.SqlTrackingService).FullName);
    }

    private const string StateMachineWorkflowMustHaveACurrentState = "StateMachineWorkflowMustHaveACurrentState";
    internal static string GetStateMachineWorkflowMustHaveACurrentState()
    {
        return GetString(StateMachineWorkflowMustHaveACurrentState,
            typeof(StateMachineWorkflowActivity).Name);
    }

    private const string InvalidActivityStatus = "InvalidActivityStatus";
    internal static string GetInvalidActivityStatus(System.Workflow.ComponentModel.Activity activity)
    {
        return GetString(InvalidActivityStatus,
            activity.ExecutionStatus,
            activity.QualifiedName);
    }

    internal const string StateMachineWorkflowRequired = "StateMachineWorkflowRequired";
    internal static string GetStateMachineWorkflowRequired()
    {
        return GetString(StateMachineWorkflowRequired,
            typeof(StateMachineWorkflowInstance).Name,
            typeof(StateMachineWorkflowActivity).Name);
    }

    private const string InvalidUserDataInStateChangeTrackingRecord = "InvalidUserDataInStateChangeTrackingRecord";
    internal static string GetInvalidUserDataInStateChangeTrackingRecord()
    {
        return GetString(InvalidUserDataInStateChangeTrackingRecord,
            StateActivity.StateChangeTrackingDataKey,
            typeof(StateActivity).Name);
    }

    private const string UnableToTransitionToState = "UnableToTransitionToState";
    internal static string GetUnableToTransitionToState(string stateName)
    {
        return GetString(UnableToTransitionToState,
            stateName);
    }

    private const string InvalidStateTransitionPath = "InvalidStateTransitionPath";
    internal static string GetInvalidStateTransitionPath()
    {
        return GetString(InvalidStateTransitionPath);
    }

    private const string InvalidSetStateInStateInitialization = "InvalidSetStateInStateInitialization";
    internal static string GetInvalidSetStateInStateInitialization()
    {
        return GetString(InvalidSetStateInStateInitialization,
            typeof(SetStateActivity).Name,
            typeof(StateInitializationActivity).Name);
    }

    private const string StateAlreadySubscribesToThisEvent = "StateAlreadySubscribesToThisEvent";
    internal static string GetStateAlreadySubscribesToThisEvent(string stateName, IComparable queueName)
    {
        return GetString(StateAlreadySubscribesToThisEvent,
            typeof(StateActivity).Name,
            stateName,
            queueName);
    }

    private const string InvalidStateMachineAction = "InvalidStateMachineAction";
    internal static string GetInvalidStateMachineAction(string stateName)
    {
        return GetString(InvalidStateMachineAction,
            typeof(StateActivity).Name,
            typeof(StateMachineAction).Name,
            stateName);
    }

    private const string Error_StateMachineWorkflowMustBeARootActivity = "Error_StateMachineWorkflowMustBeARootActivity";
    internal static string GetError_StateMachineWorkflowMustBeARootActivity()
    {
        return GetString(Error_StateMachineWorkflowMustBeARootActivity,
            typeof(StateMachineWorkflowActivity).Name);
    }

    internal const string EventHandlingScopeActivityDescription = "EventHandlingScopeActivityDescription";
    internal const string EventDrivenActivityDescription = "EventDrivenActivityDescription";
    internal const string Error_EventActivityIsImmutable = "Error_EventActivityIsImmutable";
    private const string Error_EventDrivenParentNotListen = "Error_EventDrivenParentNotListen";
    internal static string GetError_EventDrivenParentNotListen()
    {
        return GetString(Error_EventDrivenParentNotListen,
            typeof(EventDrivenActivity).Name,
            typeof(ListenActivity).Name,
            typeof(EventHandlersActivity).Name,
            typeof(StateActivity).Name,
            typeof(StateMachineWorkflowActivity).Name);
    }

    internal const string Error_EventDrivenNoFirstActivity = "Error_EventDrivenNoFirstActivity";
    private const string Error_EventDrivenInvalidFirstActivity = "Error_EventDrivenInvalidFirstActivity";
    internal static string GetError_EventDrivenInvalidFirstActivity()
    {
        return GetString(Error_EventDrivenInvalidFirstActivity,
            typeof(EventDrivenActivity).Name,
            typeof(IEventActivity).FullName,
            typeof(HandleExternalEventActivity).Name,
            typeof(DelayActivity).Name);
    }

    private const string UndoSetAsInitialState = "UndoSetAsInitialState";
    internal static string GetUndoSetAsInitialState(string stateName)
    {
        return GetString(UndoSetAsInitialState,
            stateName);
    }

    private const string UndoSetAsCompletedState = "UndoSetAsCompletedState";
    internal static string GetUndoSetAsCompletedState(string stateName)
    {
        return GetString(UndoSetAsCompletedState,
            stateName);
    }

    internal const string UndoSwitchViews = "UndoSwitchViews";

    private const string MoveSetState = "MoveSetState";
    internal static string GetMoveSetState()
    {
        return GetString(MoveSetState, typeof(SetStateActivity).Name);
    }

    internal const string Error_EventHandlersDeclParentNotScope = "Error_EventHandlersDeclParentNotScope";
    internal const string Error_EventHandlersChildNotFound = "Error_EventHandlersChildNotFound";
    internal const string Error_FailedToStartTheWorkflow = "Error_FailedToStartTheWorkflow";

    // workflow load errors


    // serializer errrors
    internal const string In = "In";
    internal const string Out = "Out";
    internal const string Ref = "Ref";
    internal const string Required = "Required";
    internal const string Optional = "Optional";
    internal const string Parameters = "Parameters";
    internal const string Properties = "Properties";
    internal const string Error_UninitializedCorrelation = "Error_UninitializedCorrelation";
    internal const string Error_InvalidIdentifier = "Error_InvalidIdentifier";
    internal const string Error_MoreThanOneEventHandlersDecl = "Error_MoreThanOneEventHandlersDecl";
    internal const string Error_MoreThanTwoActivitiesInEventHandlingScope = "Error_MoreThanTwoActivitiesInEventHandlingScope";

    //Collection Editor Resources
    internal const string Error_ExecInAtomicScope = "Error_ExecInAtomicScope";
    internal const string Error_ExecWithActivationReceive = "Error_ExecWithActivationReceive";
    internal const string Error_DuplicateParameter = "Error_DuplicateParameter";
    internal const string Error_GeneratorShouldContainSingleActivity = "Error_GeneratorShouldContainSingleActivity";

    // Dynamic Validations
    internal const string Error_DynamicActivity = "Error_DynamicActivity";
    internal const string Error_DynamicActivity2 = "Error_DynamicActivity2";
    internal const string Error_DynamicActivity3 = "Error_DynamicActivity3";

    //type filtering
    internal const string FilterDescription_InvokeWorkflow = "FilterDescription_InvokeWorkflow";

    // Activity Category
    internal const string Standard = "Standard";
    internal const string Base = "Base";

    // Themes Category
    internal const string ForegroundCategory = "ForegroundCategory";

    // Project options dialog

    //Activity Toolbox Description
    internal const string WebServiceResponseActivityDescription = "WebServiceResponseActivityDescription";
    internal const string WebServiceFaultActivityDescription = "WebServiceFaultActivityDescription";
    internal const string WebServiceReceiveActivityDescription = "WebServiceReceiveActivityDescription";
    internal const string SequenceActivityDescription = "SequenceActivityDescription";
    internal const string CompensatableSequenceActivityDescription = "CompensatableSequenceActivityDescription";

    internal const string WhileActivityDescription = "WhileActivityDescription";
    internal const string ReplicatorActivityDescription = "ReplicatorActivityDescription";
    internal const string ScopeActivityDescription = "ScopeActivityDescription";
    internal const string ParallelActivityDescription = "ParallelActivityDescription";
    internal const string ListenActivityDescription = "ListenActivityDescription";
    internal const string DelayActivityDescription = "DelayActivityDescription";
    internal const string ConstrainedGroupActivityDescription = "ConstrainedGroupActivityDescription";
    internal const string ConditionalActivityDescription = "ConditionalActivityDescription";
    internal const string InvokeWorkflowActivityDescription = "InvokeWorkflowActivityDescription";
    internal const string InvokeWebServiceActivityDescription = "InvokeWebServiceActivityDescription";
    internal const string CodeActivityDescription = "CodeActivityDescription";
    internal const string SetStateActivityDescription = "SetStateActivityDescription";
    internal const string StateInitializationActivityDescription = "StateInitializationActivityDescription";
    internal const string StateFinalizationActivityDescription = "StateFinalizationActivityDescription";
    internal const string StateActivityDescription = "StateActivityDescription";
    internal const string StateMachineWorkflowActivityDescription = "StateMachineWorkflowActivityDescription";
    internal const string PolicyActivityDescription = "PolicyActivityDescription";


    internal const string Error_WhileShouldHaveOneChild = "Error_WhileShouldHaveOneChild";

    internal const string Error_ReplicatorNotExecuting = "Error_ReplicatorNotExecuting";
    internal const string Error_ReplicatorChildRunning = "Error_ReplicatorChildRunning";
    internal const string Error_ReplicatorNotInitialized = "Error_ReplicatorNotInitialized";
    internal const string Error_ReplicatorDisconnected = "Error_ReplicatorDisconnected";
    internal const string Error_InsufficientArrayPassedIn = "Error_InsufficientArrayPassedIn";
    internal const string Error_MultiDimensionalArray = "Error_MultiDimensionalArray";

    internal const string Error_WebServiceReceiveNotValid = "Error_WebServiceReceiveNotValid";
    internal const string Error_CantInvokeSelf = "Error_CantInvokeSelf";
    internal const string Error_TypeNotPublicSerializable = "Error_TypeNotPublicSerializable";
    internal const string Error_CantInvokeDesignTimeTypes = "Error_CantInvokeDesignTimeTypes";
    internal const string Error_TypeNotPublic = "Error_TypeNotPublic";


    internal const string Error_NestedConstrainedGroupConditions = "Error_NestedConstrainedGroupConditions";
    internal const string Error_ServiceMissingExternalDataExchangeInterface = "Error_ServiceMissingExternalDataExchangeInterface";
    internal const string Error_CorrelationTokenInReplicator = "Error_CorrelationTokenInReplicator";
    internal const string HelperExternalDataExchangeDesc = "HelperExternalDataExchangeDesc";
    internal const string Error_TypePropertyInvalid = "Error_TypePropertyInvalid";
    internal const string Error_ParameterTypeNotFound = "Error_ParameterTypeNotFound";
    internal const string Error_ReturnTypeNotFound = "Error_ReturnTypeNotFound";
    internal const string TargetStateDescription = "TargetStateDescription";
    internal const string InitialStateDescription = "InitialStateDescription";
    internal const string CompletedStateDescription = "CompletedStateDescription";
    internal const string Error_CorrelationTokenMissing = "Error_CorrelationTokenMissing";
    internal const string Error_CorrelationNotInitialized = "Error_CorrelationNotInitialized";
    internal const string Error_EventDeliveryFailedException = "Error_EventDeliveryFailedException";
    internal const string Error_EventArgumentSerializationException = "Error_EventArgumentSerializationException";
    internal const string Error_ExternalDataExchangeException = "Error_ExternalDataExchangeException";
    internal const string Error_EventNameMissing = "Error_EventNameMissing";
    internal const string Error_CorrelationParameterException = "Error_CorrelationParameterException";
    internal const string Error_NoInstanceInSession = "Error_NoInstanceInSession";
    internal const string Error_ServiceNotFound = "Error_ServiceNotFound";
    internal const string Error_MissingInterfaceType = "Error_MissingInterfaceType";
    internal const string Error_CorrelationAttributeInvalid = "Error_CorrelationAttributeInvalid";
    internal const string Error_DuplicateCorrelationAttribute = "Error_DuplicateCorrelationAttribute";
    internal const string Error_CorrelationParameterNotFound = "Error_CorrelationParameterNotFound";
    internal const string Error_CorrelationInitializerNotDefinied = "Error_CorrelationInitializerNotDefinied";
    internal const string Error_InvalidMethodPropertyName = "Error_InvalidMethodPropertyName";
    internal const string Error_InvalidEventPropertyName = "Error_InvalidEventPropertyName";
    internal const string Error_CorrelationTokenSpecifiedForUncorrelatedInterface = "Error_CorrelationTokenSpecifiedForUncorrelatedInterface";
    internal const string Error_MissingCorrelationTokenOwnerNameProperty = "Error_MissingCorrelationTokenOwnerNameProperty";
    internal const string Error_OwnerActivityIsNotParent = "Error_OwnerActivityIsNotParent";
    internal const string Error_InvalidEventArgsSignature = "Error_InvalidEventArgsSignature";
    internal const string Error_NoMatchingActiveDirectoryEntry = "Error_NoMatchingActiveDirectoryEntry";
    internal const string WorkflowAuthorizationException = "WorkflowAuthorizationException";
    internal const string Error_InvalidEventMessage = "Error_InvalidEventMessage";
    internal const string Error_ExternalRuntimeContainerNotFound = "Error_ExternalRuntimeContainerNotFound";
    internal const string ExternalMethodNameDescr = "ExternalMethodNameDescr";
    internal const string ExternalEventNameDescr = "ExternalEventNameDescr";
    internal const string Error_MisMatchCorrelationTokenOwnerNameProperty = "Error_MisMatchCorrelationTokenOwnerNameProperty";
    internal const string Error_WebServiceInputNotProcessed = "Error_WebServiceInputNotProcessed";
    internal const string Error_CallExternalMethodArgsSerializationException = "Error_CallExternalMethodArgsSerializationException";
    internal const string InvokeParameterDescription = "InvokeParameterDescription";
    internal const string Error_ParameterTypeResolution = "Error_ParameterTypeResolution";
    internal const string ParameterDescription = "ParameterDescription";
    internal const string Error_DuplicatedActivityID = "Error_DuplicatedActivityID";
    internal const string Error_InvalidLanguageIdentifier = "Error_InvalidLanguageIdentifier";
    internal const string Error_ConditionalDeclNotAllConditionalBranchDecl = "Error_ConditionalDeclNotAllConditionalBranchDecl";
    internal const string Error_ConditionalLessThanOneChildren = "Error_ConditionalLessThanOneChildren";
    internal const string Error_ConditionalBranchUpdateAtRuntime = "Error_ConditionalBranchUpdateAtRuntime";
    internal const string Error_ReplicatorInvalidExecutionType = "Error_ReplicatorInvalidExecutionType";
    internal const string Error_ReplicatorCannotCancelChild = "Error_ReplicatorCannotCancelChild";
    internal const string Error_InvalidCAGActivityType = "Error_InvalidCAGActivityType";
    internal const string Error_CorrelationViolationException = "Error_CorrelationViolationException";
    internal const string Error_CannotResolveWebServiceInput = "Error_CannotResolveWebServiceInput";
    internal const string Error_InvalidLocalServiceMessage = "Error_InvalidLocalServiceMessage";
    internal const string Error_InitializerInReplicator = "Error_InitializerInReplicator";
    internal const string CodeConditionDisplayName = "CodeConditionDisplayName";
    internal const string RuleConditionDisplayName = "RuleConditionDisplayName";
    internal const string Error_InterfaceTypeNeedsExternalDataExchangeAttribute = "Error_InterfaceTypeNeedsExternalDataExchangeAttribute";
    internal const string Error_WorkflowInstanceDehydratedBeforeSendingResponse = "Error_InstanceDehydratedBeforeSendingResponse";
    internal const string Error_InitializerFollowerInTxnlScope = "Error_InitializerFollowerInTxnlScope";
    internal const string ShowingExternalDataExchangeService = "ShowingExternalDataExchangeService";
    internal const string InterfaceTypeMissing = "InterfaceTypeMissing";
    internal const string MethodNameMissing = "MethodNameMissing";
    internal const string MethodInfoMissing = "MethodInfoMissing";
    internal const string EventNameMissing = "EventNameMissing";
    internal const string EventInfoMissing = "EventInfoMissing";
    internal const string HandleExternalEventActivityDescription = "HandleExternalEventActivityDescription";
    internal const string CallExternalMethodActivityDescription = "CallExternalMethodActivityDescription";
    internal const string Error_EventArgumentValidationException = "Error_EventArgumentValidationException";
    internal const string Error_GenericMethodsNotSupported = "Error_GenericMethodsNotSupported";
    internal const string Error_ReturnTypeNotVoid = "Error_ReturnTypeNotVoid";
    internal const string Error_OutRefParameterNotSupported = "Error_OutRefParameterNotSupported";
    internal const string InvalidTimespanFormat = "InvalidTimespanFormat";
    internal const string Error_CantFindInstance = "Error_CantFindInstance";

    internal static string Error_SenderMustBeActivityExecutionContext
    {
        get
        {
            return GetString("Error_SenderMustBeActivityExecutionContext", typeof(System.Workflow.ComponentModel.ActivityExecutionContext).FullName);
        }
    }
}

