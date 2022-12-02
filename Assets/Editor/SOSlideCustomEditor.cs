using System;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SO_Slide)), CanEditMultipleObjects]
public class SOSlideCustomEditor : Editor
{
    private SerializedProperty
        _sliderTypeProperty,
        _headerProperty,
        _footerProperty,
        _assignmentTimeProperty,
        _timeIsChangeableByPlayerCountProperty,
        _backgroundImageProperty,
        _headerImageProperty,
        _audioFragmentProperty,
        _footerBackgroundProperty,
        _timerBackgroundProperty,
        _previousButtonProperty,
        _nextButtonProperty;

    private void OnEnable()
    {
        _sliderTypeProperty = serializedObject.FindProperty("slideType");
        _headerProperty = serializedObject.FindProperty("header");
        _footerProperty = serializedObject.FindProperty("footer");
        _assignmentTimeProperty = serializedObject.FindProperty("assignmentTime");
        _timeIsChangeableByPlayerCountProperty = serializedObject.FindProperty("timeIsChangeableByPlayerCount");
        _backgroundImageProperty = serializedObject.FindProperty("backgroundImage");
        _headerImageProperty = serializedObject.FindProperty("headerImage");
        _audioFragmentProperty = serializedObject.FindProperty("audioFragment");
        _footerBackgroundProperty = serializedObject.FindProperty("footerBackgroundImage");
        _timerBackgroundProperty = serializedObject.FindProperty("timerBackgroundImage");
        _previousButtonProperty = serializedObject.FindProperty("previousButtonImage");
        _nextButtonProperty = serializedObject.FindProperty("nextButtonImage");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_sliderTypeProperty);

        var st = (SO_Slide.SlideType)_sliderTypeProperty.enumValueIndex;

        switch (st)
        {
            case SO_Slide.SlideType.ThemeTitleScreen:
                EditorGUILayout.PropertyField(_backgroundImageProperty, new GUIContent(_backgroundImageProperty.displayName));
                EditorGUILayout.PropertyField(_previousButtonProperty, new GUIContent(_previousButtonProperty.displayName));
                EditorGUILayout.PropertyField(_nextButtonProperty, new GUIContent(_nextButtonProperty.displayName));
                break;
            
            case SO_Slide.SlideType.ThemeOverview:
                EditorGUILayout.PropertyField(_headerProperty, new GUIContent(_headerProperty.displayName));
                EditorGUILayout.PropertyField(_footerProperty, new GUIContent(_footerProperty.displayName));
                EditorGUILayout.PropertyField(_backgroundImageProperty, new GUIContent(_backgroundImageProperty.displayName));
                EditorGUILayout.PropertyField(_footerBackgroundProperty, new GUIContent(_footerBackgroundProperty.displayName));
                EditorGUILayout.PropertyField(_timerBackgroundProperty, new GUIContent(_timerBackgroundProperty.displayName));
                EditorGUILayout.PropertyField(_previousButtonProperty, new GUIContent(_previousButtonProperty.displayName));
                EditorGUILayout.PropertyField(_nextButtonProperty, new GUIContent(_nextButtonProperty.displayName));
                break;

            case SO_Slide.SlideType.AssignmentInfo:
                EditorGUILayout.PropertyField(_headerProperty, new GUIContent(_headerProperty.displayName));
                EditorGUILayout.PropertyField(_footerProperty, new GUIContent(_footerProperty.displayName));
                EditorGUILayout.PropertyField(_assignmentTimeProperty, new GUIContent(_assignmentTimeProperty.displayName));
                EditorGUILayout.PropertyField(_timeIsChangeableByPlayerCountProperty, new GUIContent(_timeIsChangeableByPlayerCountProperty.displayName));
                EditorGUILayout.PropertyField(_backgroundImageProperty, new GUIContent(_backgroundImageProperty.displayName));
                EditorGUILayout.PropertyField(_headerImageProperty, new GUIContent(_headerImageProperty.displayName));
                EditorGUILayout.PropertyField(_audioFragmentProperty, new GUIContent(_audioFragmentProperty.displayName));
                EditorGUILayout.PropertyField(_footerBackgroundProperty, new GUIContent(_footerBackgroundProperty.displayName));
                EditorGUILayout.PropertyField(_timerBackgroundProperty, new GUIContent(_timerBackgroundProperty.displayName));
                EditorGUILayout.PropertyField(_previousButtonProperty, new GUIContent(_previousButtonProperty.displayName));
                EditorGUILayout.PropertyField(_nextButtonProperty, new GUIContent(_nextButtonProperty.displayName));
                break;

            case SO_Slide.SlideType.AssignmentFeedback:
                EditorGUILayout.PropertyField(_headerProperty, new GUIContent(_headerProperty.displayName));
                EditorGUILayout.PropertyField(_footerProperty, new GUIContent(_footerProperty.displayName));
                EditorGUILayout.PropertyField(_assignmentTimeProperty, new GUIContent(_assignmentTimeProperty.displayName));
                EditorGUILayout.PropertyField(_timeIsChangeableByPlayerCountProperty, new GUIContent(_timeIsChangeableByPlayerCountProperty.displayName));
                EditorGUILayout.PropertyField(_backgroundImageProperty, new GUIContent(_backgroundImageProperty.displayName));
                EditorGUILayout.PropertyField(_headerImageProperty, new GUIContent(_headerImageProperty.displayName));
                EditorGUILayout.PropertyField(_footerBackgroundProperty, new GUIContent(_footerBackgroundProperty.displayName));
                EditorGUILayout.PropertyField(_timerBackgroundProperty, new GUIContent(_timerBackgroundProperty.displayName));
                EditorGUILayout.PropertyField(_previousButtonProperty, new GUIContent(_previousButtonProperty.displayName));
                EditorGUILayout.PropertyField(_nextButtonProperty, new GUIContent(_nextButtonProperty.displayName));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
