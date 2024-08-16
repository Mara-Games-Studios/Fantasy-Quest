/******************************************************************************
 * Spine Runtimes License Agreement
 * Last updated January 1, 2020. Replaces all prior versions.
 *
 * Copyright (c) 2013-2020, Esoteric Software LLC
 *
 * Integration of the Spine Runtimes into software or otherwise creating
 * derivative works of the Spine Runtimes is permitted under the terms and
 * conditions of Section 2 of the Spine Editor License Agreement:
 * http://esotericsoftware.com/spine-editor-license
 *
 * Otherwise, it is permitted to integrate the Spine Runtimes into software
 * or otherwise create derivative works of the Spine Runtimes (collectively,
 * "Products"), provided that each user of the Products must obtain their own
 * Spine Editor license and redistribution of the Products in any form must
 * include this license and copyright notice.
 *
 * THE SPINE RUNTIMES ARE PROVIDED BY ESOTERIC SOFTWARE LLC "AS IS" AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL ESOTERIC SOFTWARE LLC BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES,
 * BUSINESS INTERRUPTION, OR LOSS OF USE, DATA, OR PROFITS) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THE SPINE RUNTIMES, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/

using UnityEditor;
using UnityEngine;
using SpineInspectorUtility = Spine.Unity.Editor.SpineInspectorUtility;

public class SpineShaderWithOutlineGUI : ShaderGUI
{
    protected MaterialEditor _materialEditor;
    private bool _showAdvancedOutlineSettings = false;
    private bool _showStencilSettings = false;
    private MaterialProperty _OutlineWidth = null;
    private MaterialProperty _OutlineColor = null;
    private MaterialProperty _OutlineReferenceTexWidth = null;
    private MaterialProperty _ThresholdEnd = null;
    private MaterialProperty _OutlineSmoothness = null;
    private MaterialProperty _Use8Neighbourhood = null;
    private MaterialProperty _OutlineMipLevel = null;
    private MaterialProperty _StencilComp = null;
    private MaterialProperty _StencilRef = null;
    private static GUIContent _EnableOutlineText =
        new(
            "Outline",
            "Enable outline rendering. Draws an outline by sampling 4 or 8 neighbourhood pixels at a given distance specified via 'Outline Width'."
        );
    private static GUIContent _OutlineWidthText = new("Outline Width", "");
    private static GUIContent _OutlineColorText = new("Outline Color", "");
    private static GUIContent _OutlineReferenceTexWidthText = new("Reference Texture Width", "");
    private static GUIContent _ThresholdEndText = new("Outline Threshold", "");
    private static GUIContent _OutlineSmoothnessText = new("Outline Smoothness", "");
    private static GUIContent _Use8NeighbourhoodText = new("Sample 8 Neighbours", "");
    private static GUIContent _OutlineMipLevelText = new("Outline Mip Level", "");
    private static GUIContent _StencilCompText = new("Stencil Comparison", "");
    private static GUIContent _StencilRefText = new("Stencil Reference", "");
    private static GUIContent _OutlineAdvancedText = new("Advanced", "");
    private static GUIContent _ShowStencilText =
        new("Stencil", "Stencil parameters for mask interaction.");

    protected const string ShaderOutlineNamePrefix = "Spine/Outline/";
    protected const string ShaderNormalNamePrefix = "Spine/";
    protected const string ShaderWithoutStandardVariantSuffix = "OutlineOnly";

    #region ShaderGUI

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        FindProperties(properties); // MaterialProperties can be animated so we do not cache them but fetch them every event to ensure animated values are updated correctly
        _materialEditor = materialEditor;

        base.OnGUI(materialEditor, properties);
        EditorGUILayout.Space();
        RenderStencilProperties();
        EditorGUILayout.Space();
        RenderOutlineProperties();
    }

    #endregion

    #region Virtual Interface

    protected virtual void FindProperties(MaterialProperty[] props)
    {
        _OutlineWidth = FindProperty("_OutlineWidth", props, false);
        _OutlineReferenceTexWidth = FindProperty("_OutlineReferenceTexWidth", props, false);
        _OutlineColor = FindProperty("_OutlineColor", props, false);
        _ThresholdEnd = FindProperty("_ThresholdEnd", props, false);
        _OutlineSmoothness = FindProperty("_OutlineSmoothness", props, false);
        _Use8Neighbourhood = FindProperty("_Use8Neighbourhood", props, false);
        _OutlineMipLevel = FindProperty("_OutlineMipLevel", props, false);

        _StencilComp = FindProperty("_StencilComp", props, false);
        _StencilRef = FindProperty("_StencilRef", props, false);
        _StencilRef ??= FindProperty("_Stencil", props, false);
    }

    protected virtual void RenderStencilProperties()
    {
        if (_StencilComp == null)
        {
            return; // not a shader supporting custom stencil operations
        }

        // Use default labelWidth
        EditorGUIUtility.labelWidth = 0f;
        _showStencilSettings = EditorGUILayout.Foldout(_showStencilSettings, _ShowStencilText);
        if (_showStencilSettings)
        {
            using (new SpineInspectorUtility.IndentScope())
            {
                _materialEditor.ShaderProperty(_StencilComp, _StencilCompText);
                _materialEditor.ShaderProperty(_StencilRef, _StencilRefText);
            }
        }
    }

    protected virtual void RenderOutlineProperties()
    {
        if (_OutlineWidth == null)
        {
            return; // not an outline shader
        }

        // Use default labelWidth
        EditorGUIUtility.labelWidth = 0f;
        bool hasOutlineVariant = !IsShaderWithoutStandardVariantShader(_materialEditor, out _);
        bool isOutlineEnabled = true;
        if (hasOutlineVariant)
        {
            isOutlineEnabled = IsOutlineEnabled(_materialEditor, out bool mixedValue);
            EditorGUI.showMixedValue = mixedValue;
            EditorGUI.BeginChangeCheck();

            FontStyle origFontStyle = EditorStyles.label.fontStyle;
            EditorStyles.label.fontStyle = FontStyle.Bold;
            isOutlineEnabled = EditorGUILayout.Toggle(_EnableOutlineText, isOutlineEnabled);
            EditorStyles.label.fontStyle = origFontStyle;
            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                foreach (Material material in _materialEditor.targets)
                {
                    SwitchShaderToOutlineSettings(material, isOutlineEnabled);
                }
            }
        }
        else
        {
            FontStyle origFontStyle = EditorStyles.label.fontStyle;
            EditorStyles.label.fontStyle = FontStyle.Bold;
            EditorGUILayout.LabelField(_EnableOutlineText);
            EditorStyles.label.fontStyle = origFontStyle;
        }

        if (isOutlineEnabled)
        {
            _materialEditor.ShaderProperty(_OutlineWidth, _OutlineWidthText);
            _materialEditor.ShaderProperty(_OutlineColor, _OutlineColorText);

            _showAdvancedOutlineSettings = EditorGUILayout.Foldout(
                _showAdvancedOutlineSettings,
                _OutlineAdvancedText
            );
            if (_showAdvancedOutlineSettings)
            {
                using (new SpineInspectorUtility.IndentScope())
                {
                    _materialEditor.ShaderProperty(
                        _OutlineReferenceTexWidth,
                        _OutlineReferenceTexWidthText
                    );
                    _materialEditor.ShaderProperty(_ThresholdEnd, _ThresholdEndText);
                    _materialEditor.ShaderProperty(_OutlineSmoothness, _OutlineSmoothnessText);
                    _materialEditor.ShaderProperty(_Use8Neighbourhood, _Use8NeighbourhoodText);
                    _materialEditor.ShaderProperty(_OutlineMipLevel, _OutlineMipLevelText);
                }
            }
        }
    }

    #endregion

    #region Private Functions

    private void SwitchShaderToOutlineSettings(Material material, bool enableOutline)
    {
        string shaderName = material.shader.name;
        bool isSetToOutlineShader = shaderName.Contains(ShaderOutlineNamePrefix);
        if (isSetToOutlineShader && !enableOutline)
        {
            shaderName = shaderName.Replace(ShaderOutlineNamePrefix, ShaderNormalNamePrefix);
            _materialEditor.SetShader(Shader.Find(shaderName), false);
            return;
        }
        else if (!isSetToOutlineShader && enableOutline)
        {
            shaderName = shaderName.Replace(ShaderNormalNamePrefix, ShaderOutlineNamePrefix);
            _materialEditor.SetShader(Shader.Find(shaderName), false);
            return;
        }
    }

    private static bool IsOutlineEnabled(MaterialEditor editor, out bool mixedValue)
    {
        mixedValue = false;
        bool isAnyEnabled = false;
        foreach (Material material in editor.targets)
        {
            if (material.shader.name.Contains(ShaderOutlineNamePrefix))
            {
                isAnyEnabled = true;
            }
            else if (isAnyEnabled)
            {
                mixedValue = true;
            }
        }
        return isAnyEnabled;
    }

    private static bool IsShaderWithoutStandardVariantShader(
        MaterialEditor editor,
        out bool mixedValue
    )
    {
        mixedValue = false;
        bool isAnyShaderWithoutVariant = false;
        foreach (Material material in editor.targets)
        {
            if (material.shader.name.Contains(ShaderWithoutStandardVariantSuffix))
            {
                isAnyShaderWithoutVariant = true;
            }
            else if (isAnyShaderWithoutVariant)
            {
                mixedValue = true;
            }
        }
        return isAnyShaderWithoutVariant;
    }

    private static bool BoldToggleField(GUIContent label, bool value)
    {
        FontStyle origFontStyle = EditorStyles.label.fontStyle;
        EditorStyles.label.fontStyle = FontStyle.Bold;
        value = EditorGUILayout.Toggle(label, value, EditorStyles.toggle);
        EditorStyles.label.fontStyle = origFontStyle;
        return value;
    }

    #endregion
}
