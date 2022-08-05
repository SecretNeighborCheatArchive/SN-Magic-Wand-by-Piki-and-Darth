using GameModes.GameplayMode.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats
{
    internal static class ESPUtils
    {
        private static GUIStyle _style = new GUIStyle();

        private static Texture2D drawingTex;
        private static Color lastTexColour;

        private static Rect lineRect = new Rect(0f, 0f, 1f, 1f);
        private static Texture2D lineTex = null;
        private static Texture2D aaLineTex = null;
        private static Material blendMaterial = null;
        private static Material blitMaterial = null;
        private static Material drawMaterial;


        internal static void Init()
        {
            drawMaterial = new Material(Shader.Find("Hidden/Internal-Colored"))
            {
                hideFlags = (HideFlags)61
            };

            drawMaterial.SetInt("_SrcBlend", 5);
            drawMaterial.SetInt("_DstBlend", 10);
            drawMaterial.SetInt("_Cull", 0);
            drawMaterial.SetInt("_ZWrite", 0);
        }

        internal static void Label(Rect position, string text, Color colour)
        {
            Color oldColour = GUI.color;

            GUI.color = colour;

            GUI.Label(position, text);

            GUI.color = oldColour;
        }

        internal static void ShadowedLabel(Rect rect, string text, Color textCol, Vector2 size)
        {
            _style.normal.textColor = Color.black;

            rect.x += size.x;
            rect.y += size.y;

            GUI.Label(rect, text, _style);

            _style.normal.textColor = textCol;

            rect.x -= size.x;
            rect.y -= size.y;

            GUI.Label(rect, text, _style);
        }

        internal static void DrawBox(Vector2 pos, Vector2 size, Color color)
        {
            if (!drawingTex)
                drawingTex = new Texture2D(1, 1);
            
            if (color != lastTexColour)
            {
                drawingTex.SetPixel(0, 0, color);
                drawingTex.Apply();

                lastTexColour = color;
            }

            GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y), drawingTex);
        }

        internal static void BoxString(GUIContent text, Vector2 w2s, Color textColor, float padding = 1.25f, float opacity = (255f / 2f) / 255f)
        {
            Color boxColor = Color.black;
            boxColor.a = opacity;

            Vector2 textSize = CalcSize(text);
            Vector2 boxPos = new Vector2(w2s.x - padding, w2s.y - padding);

            DrawBox(boxPos, new Vector2(textSize.x + padding, textSize.y + padding), boxColor);
            DrawString1(w2s, text.text, textColor, false, 10, FontStyle.Bold, 0);
        }

        internal static void DrawBoxOutlines(Vector2 position, Vector2 size, float borderSize, Color color)
        {
            DrawBox(new Vector2(position.x + borderSize, position.y), new Vector2(size.x - 2f * borderSize, borderSize), color);
            DrawBox(new Vector2(position.x, position.y), new Vector2(borderSize, size.y), color);
            DrawBox(new Vector2(position.x + size.x - borderSize, position.y), new Vector2(borderSize, size.y), color);
            DrawBox(new Vector2(position.x + borderSize, position.y + size.y - borderSize), new Vector2(size.x - 2f * borderSize, borderSize), color);
        }

        internal static void DrawLineShadow(Vector2 start, Vector2 end, Color color, float width, float shadowSize = 2f)
        {
            DrawLine(new Vector2(start.x - shadowSize, start.y - shadowSize), new Vector2(start.x - shadowSize, start.y - shadowSize), color, width);
            DrawLine(new Vector2(start.x + shadowSize, start.y + shadowSize), new Vector2(start.x + shadowSize, start.y + shadowSize), color, width);
            DrawLine(start, end, color, width);
        }

        internal static void DrawLine(Vector2 start, Vector2 end, Color color, float width)
        {
            Color oldColour = GUI.color;

            var rad2deg = 360 / (Math.PI * 2);

            Vector2 d = end - start;

            float a = (float)rad2deg * Mathf.Atan(d.y / d.x);

            if (d.x < 0)
                a += 180;

            int width2 = (int)Mathf.Ceil(width / 2);

            GUIUtility.RotateAroundPivot(a, start);

            GUI.color = color;

            GUI.DrawTexture(new Rect(start.x, start.y - width2, d.magnitude, width), Texture2D.whiteTexture, ScaleMode.StretchToFill);

            GUIUtility.RotateAroundPivot(-a, start);

            GUI.color = oldColour;
        }

        internal static void DrawCircle(Color Col, Vector2 Center, float Radius)
        {
            GL.PushMatrix();

            if (!drawMaterial.SetPass(0))
            {
                GL.PopMatrix();
                return;
            }
            
            GL.Begin(1);
            GL.Color(Col);

            for (float num = 0f; num < 6.28318548f; num += 0.05f)
            {
                GL.Vertex(new Vector3(Mathf.Cos(num) * Radius + Center.x, Mathf.Sin(num) * Radius + Center.y));
                GL.Vertex(new Vector3(Mathf.Cos(num + 0.05f) * Radius + Center.x, Mathf.Sin(num + 0.05f) * Radius + Center.y));
            }

            GL.End();
            GL.PopMatrix();
        }

        internal static void Draw3DBox(Color color, UnityEngine.Object obj)
        {
            /*Vector3[] pts = new Vector3[8];
            pts[0] = mainCam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z));
            pts[1] = mainCam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z));
            pts[2] = mainCam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z));
            pts[3] = mainCam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z));
            pts[4] = mainCam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z));
            pts[5] = mainCam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z));
            pts[6] = mainCam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z));
            pts[7] = mainCam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z));

            for (int i = 0; i < pts.Length; i++) 
                pts[i].y = Screen.height - pts[i].y;

            /*GL.PushMatrix();
            GL.Begin(1);
            drawMaterial.SetPass(0);
            GL.End();
            GL.PopMatrix();*/

            /*GL.PushMatrix();
            GL.Begin(1);
            if (!drawMaterial.SetPass(0))
            {
                GL.End();
                GL.PopMatrix();
                return;
            }
            GL.Color(color);
            GL.Vertex3(pts[0].x, pts[0].y, 0f);
            GL.Vertex3(pts[1].x, pts[1].y, 0f);
            GL.Vertex3(pts[1].x, pts[1].y, 0f);
            GL.Vertex3(pts[5].x, pts[5].y, 0f);
            GL.Vertex3(pts[5].x, pts[5].y, 0f);
            GL.Vertex3(pts[4].x, pts[4].y, 0f);
            GL.Vertex3(pts[4].x, pts[4].y, 0f);
            GL.Vertex3(pts[0].x, pts[0].y, 0f);
            GL.Vertex3(pts[2].x, pts[2].y, 0f);
            GL.Vertex3(pts[3].x, pts[3].y, 0f);
            GL.Vertex3(pts[3].x, pts[3].y, 0f);
            GL.Vertex3(pts[7].x, pts[7].y, 0f);
            GL.Vertex3(pts[7].x, pts[7].y, 0f);
            GL.Vertex3(pts[6].x, pts[6].y, 0f);
            GL.Vertex3(pts[6].x, pts[6].y, 0f);
            GL.Vertex3(pts[2].x, pts[2].y, 0f);
            GL.Vertex3(pts[2].x, pts[2].y, 0f);
            GL.Vertex3(pts[0].x, pts[0].y, 0f);
            GL.Vertex3(pts[3].x, pts[3].y, 0f);
            GL.Vertex3(pts[1].x, pts[1].y, 0f);
            GL.Vertex3(pts[7].x, pts[7].y, 0f);
            GL.Vertex3(pts[5].x, pts[5].y, 0f);
            GL.Vertex3(pts[6].x, pts[6].y, 0f);
            GL.Vertex3(pts[4].x, pts[4].y, 0f);

            GL.End();
            GL.PopMatrix();*/

            /* bottom
            var p1 = new Vector3(b.min.x, b.min.y, b.min.z);
            var p2 = new Vector3(b.max.x, b.min.y, b.min.z);
            var p3 = new Vector3(b.max.x, b.min.y, b.max.z);
            var p4 = new Vector3(b.min.x, b.min.y, b.max.z);

            Debug.DrawLine(p1, p2, Color.blue, 0f);
            Debug.DrawLine(p2, p3, Color.red, 0f);
            Debug.DrawLine(p3, p4, Color.yellow, 0f);
            Debug.DrawLine(p4, p1, Color.magenta, 0f);
            
            // top
            var p5 = new Vector3(b.min.x, b.max.y, b.min.z);
            var p6 = new Vector3(b.max.x, b.max.y, b.min.z);
            var p7 = new Vector3(b.max.x, b.max.y, b.max.z);
            var p8 = new Vector3(b.min.x, b.max.y, b.max.z);

            Debug.DrawLine(p5, p6, Color.blue, 0f);
            Debug.DrawLine(p6, p7, Color.red, 0f);
            Debug.DrawLine(p7, p8, Color.yellow, 0f);
            Debug.DrawLine(p8, p5, Color.magenta, 0f);

            // sides
            Debug.DrawLine(p1, p5, Color.white, 0f);
            Debug.DrawLine(p2, p6, Color.gray, 0f);
            Debug.DrawLine(p3, p7, Color.green, 0f);
            Debug.DrawLine(p4, p8, Color.cyan, 0f);*/

            /*Gizmos.color = color;
            foreach (var mf in obj.GetComponentsInChildren<MeshFilter>())
            {
                Gizmos.matrix = mf.transform.localToWorldMatrix;
                Mesh m = mf.sharedMesh;
                Gizmos.DrawWireCube(m.bounds.center, m.bounds.size);
            }*/
        }

        internal static Camera mainCam = Camera.main;
        internal static void DrawSnapline(Vector3 worldpos, Color color)
        {
            Vector3 pos = mainCam.WorldToScreenPoint(worldpos);
            pos.y = Screen.height - pos.y;
            GL.PushMatrix();
            GL.Begin(1);
            if (drawMaterial.SetPass(0))
            {
                GL.Color(color);
                GL.Vertex3(Screen.width / 2, Screen.height, 0f);
                GL.Vertex3(pos.x, pos.y, 0f);
                GL.End();
                GL.PopMatrix();
                return;
            }
            GL.End();
            GL.PopMatrix();
        }

        internal static void DrawLine3(Vector2 pointA, Vector2 pointB, Color color, float width, bool antiAlias)
        {
            float num = pointB.x - pointA.x;
            float num2 = pointB.y - pointA.y;
            float num3 = Mathf.Sqrt(num * num + num2 * num2);
            bool flag = num3 < 0.001f;
            if (!flag)
            {
                Texture2D texture2D;
                if (antiAlias)
                {
                    width *= 3f;
                    texture2D = aaLineTex;
                    Material material = blendMaterial;
                }
                else
                {
                    texture2D = lineTex;
                    Material material = blitMaterial;
                }
                float num4 = width * num2 / num3;
                float num5 = width * num / num3;
                Matrix4x4 identity = Matrix4x4.identity;
                identity.m00 = num;
                identity.m01 = -num4;
                identity.m03 = pointA.x + 0.5f * num4;
                identity.m10 = num2;
                identity.m11 = num5;
                identity.m13 = pointA.y - 0.5f * num5;
                GL.PushMatrix();
                GL.MultMatrix(identity);
                GUI.color = color;
                GUI.DrawTexture(lineRect, texture2D);
                GL.PopMatrix();
            }
        }

        internal static void DrawBezierLine(Vector2 start, Vector2 startTangent, Vector2 end, Vector2 endTangent, Color color, float width, bool antiAlias, int segments)
        {
            Vector2 pointA = CubeBezier(start, startTangent, end, endTangent, 0f);
            for (int i = 1; i < segments + 1; i++)
            {
                Vector2 vector = CubeBezier(start, startTangent, end, endTangent, (float)i / (float)segments);
                DrawLine3(pointA, vector, color, width, antiAlias);
                pointA = vector;
            }
        }

        private static Vector2 CubeBezier(Vector2 s, Vector2 st, Vector2 e, Vector2 et, float t)
        {
            float num = 1f - t;
            return num * num * num * s + 3f * num * num * t * st + 3f * num * t * t * et + t * t * t * e;
        }

        internal static void CornerBox(Vector2 Head, float Width, float Height, float thickness, Color color, bool outline)
        {
            int num = (int)(Width / 4f);
            int num2 = num;

            if (outline)
            {
                RectFilled(Head.x - Width / 2f - 1f, Head.y - 1f, (float)(num + 2), 3f, Color.black);
                RectFilled(Head.x - Width / 2f - 1f, Head.y - 1f, 3f, (float)(num2 + 2), Color.black);
                RectFilled(Head.x + Width / 2f - (float)num - 1f, Head.y - 1f, (float)(num + 2), 3f, Color.black);
                RectFilled(Head.x + Width / 2f - 1f, Head.y - 1f, 3f, (float)(num2 + 2), Color.black);
                RectFilled(Head.x - Width / 2f - 1f, Head.y + Height - 4f, (float)(num + 2), 3f, Color.black);
                RectFilled(Head.x - Width / 2f - 1f, Head.y + Height - (float)num2 - 4f, 3f, (float)(num2 + 2), Color.black);
                RectFilled(Head.x + Width / 2f - (float)num - 1f, Head.y + Height - 4f, (float)(num + 2), 3f, Color.black);
                RectFilled(Head.x + Width / 2f - 1f, Head.y + Height - (float)num2 - 4f, 3f, (float)(num2 + 3), Color.black);
            }

            RectFilled(Head.x - Width / 2f, Head.y, num, 1f, color);
            RectFilled(Head.x - Width / 2f, Head.y, 1f, num2, color);
            RectFilled(Head.x + Width / 2f - num, Head.y, num, 1f, color);
            RectFilled(Head.x + Width / 2f, Head.y, 1f, num2, color);
            RectFilled(Head.x - Width / 2f, Head.y + Height - 3f, num, 1f, color);
            RectFilled(Head.x - Width / 2f, Head.y + Height - num2 - 3f, 1f, num2, color);
            RectFilled(Head.x + Width / 2f - num, Head.y + Height - 3f, num, 1f, color);
            RectFilled(Head.x + Width / 2f, Head.y + Height - num2 - 3f, 1f, num2 + 1, color);
        }

        internal static void RectFilled(float x, float y, float width, float height, Color color)
        {
            if (!drawingTex)
                drawingTex = new Texture2D(1, 1);

            if (color != lastTexColour)
            {
                drawingTex.SetPixel(0, 0, color);
                drawingTex.Apply();

                lastTexColour = color;
            }

            GUI.DrawTexture(new Rect(x, y, width, height), drawingTex);
        }

        internal static bool IsOnScreen(Vector3 position)
        {
            return position.y > 0.01f && position.y < Screen.height - 5f && position.z > 0.01f;
        }

        private static Color __color;
        internal static void BoxRect(Rect rect, Color color)
        {
            if (color != __color)
            {
                drawingTex.SetPixel(0, 0, color);
                drawingTex.Apply();
                __color = color;
            }

            GUI.DrawTexture(rect, drawingTex);
        }

        internal static void DrawHealth1(Vector2 pos, float health, bool center = false)
        {
            if (center)
            {
                pos -= new Vector2(26f, 0f);
            }
            pos += new Vector2(0f, 18f);
            BoxRect(new Rect(pos.x, pos.y, 52f, 5f), Color.black);
            pos += new Vector2(1f, 1f);
            Color color = Color.green;
            if (health <= 50f)
            {
                color = Color.yellow;
            }
            if (health <= 25f)
            {
                color = Color.red;
            }
            BoxRect(new Rect(pos.x, pos.y, 0.5f * health, 3f), color);
        }

        private static GUIStyle ___style = new GUIStyle();
        internal static void Box(Rect position, string text, Color textColor)
        {
            ___style.fontSize = 12;
            ___style.richText = true;
            ___style.normal.textColor = textColor;
            ___style.fontStyle = FontStyle.Bold;

            GUI.Box(position, text, ___style);
        }

        internal static Vector2 CalcSize(GUIContent content)
        {
            return __style.CalcSize(content);
        }

        private static GUIStyle __style = new GUIStyle();
        private static GUIStyle __outlineStyle = new GUIStyle();
        //private static Font tahoma = Font.CreateDynamicFontFromOSFont("Tahoma", 12);
        internal static void DrawString1(Vector2 pos, string text, Color color, bool center = true, int size = 12, FontStyle fontStyle = FontStyle.Bold, int depth = 1)
        {
            __style.fontSize = size;
            __style.richText = true;
            //__style.font = tahoma;
            __style.normal.textColor = color;
            __style.fontStyle = fontStyle;

            __outlineStyle.fontSize = size;
            __outlineStyle.richText = true;
            //__outlineStyle.font = tahoma;
            __outlineStyle.normal.textColor = new Color(0f, 0f, 0f, 1f);
            __outlineStyle.fontStyle = fontStyle;

            GUIContent content = new GUIContent(text);
            GUIContent content2 = new GUIContent(text);
            if (center)
            {
                //GUI.skin.label.alignment = TextAnchor.MiddleCenter;
                pos.x -= __style.CalcSize(content).x / 2f;
            }
            switch (depth)
            {
                case 0:
                    GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), content, __style);
                    break;
                case 1:
                    GUI.Label(new Rect(pos.x + 1f, pos.y + 1f, 300f, 25f), content2, __outlineStyle);
                    GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), content, __style);
                    break;
                case 2:
                    GUI.Label(new Rect(pos.x + 1f, pos.y + 1f, 300f, 25f), content2, __outlineStyle);
                    GUI.Label(new Rect(pos.x - 1f, pos.y - 1f, 300f, 25f), content2, __outlineStyle);
                    GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), content, __style);
                    break;
                case 3:
                    content2.m_Text = $"<color=black>{content2.text}</color>";

                    GUI.Label(new Rect(pos.x + 1f, pos.y + 1f, 300f, 25f), content2, __outlineStyle);
                    GUI.Label(new Rect(pos.x - 1f, pos.y - 1f, 300f, 25f), content2, __outlineStyle);
                    GUI.Label(new Rect(pos.x, pos.y - 1f, 300f, 25f), content2, __outlineStyle);
                    GUI.Label(new Rect(pos.x, pos.y + 1f, 300f, 25f), content2, __outlineStyle);
                    GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), content, __style);
                    break;
            }
        }

        internal static void Draw3DBox(Bounds b, Color color)
        {
            Vector3[] pts = new Vector3[8];

            pts[0] = Camera.main.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z));
            pts[1] = Camera.main.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z));
            pts[2] = Camera.main.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z));
            pts[3] = Camera.main.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z));
            pts[4] = Camera.main.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z));
            pts[5] = Camera.main.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z));
            pts[6] = Camera.main.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z));
            pts[7] = Camera.main.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z));

            for (int i = 0; i < pts.Length; i++) 
                pts[i].y = Screen.height - pts[i].y;

            GL.PushMatrix();
            GL.Begin(1);
            drawMaterial.SetPass(0);
            GL.End();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Begin(1);
            drawMaterial.SetPass(0);
            GL.Color(color);
            GL.Vertex3(pts[0].x, pts[0].y, 0f);
            GL.Vertex3(pts[1].x, pts[1].y, 0f);
            GL.Vertex3(pts[1].x, pts[1].y, 0f);
            GL.Vertex3(pts[5].x, pts[5].y, 0f);
            GL.Vertex3(pts[5].x, pts[5].y, 0f);
            GL.Vertex3(pts[4].x, pts[4].y, 0f);
            GL.Vertex3(pts[4].x, pts[4].y, 0f);
            GL.Vertex3(pts[0].x, pts[0].y, 0f);
            GL.Vertex3(pts[2].x, pts[2].y, 0f);
            GL.Vertex3(pts[3].x, pts[3].y, 0f);
            GL.Vertex3(pts[3].x, pts[3].y, 0f);
            GL.Vertex3(pts[7].x, pts[7].y, 0f);
            GL.Vertex3(pts[7].x, pts[7].y, 0f);
            GL.Vertex3(pts[6].x, pts[6].y, 0f);
            GL.Vertex3(pts[6].x, pts[6].y, 0f);
            GL.Vertex3(pts[2].x, pts[2].y, 0f);
            GL.Vertex3(pts[2].x, pts[2].y, 0f);
            GL.Vertex3(pts[0].x, pts[0].y, 0f);
            GL.Vertex3(pts[3].x, pts[3].y, 0f);
            GL.Vertex3(pts[1].x, pts[1].y, 0f);
            GL.Vertex3(pts[7].x, pts[7].y, 0f);
            GL.Vertex3(pts[5].x, pts[5].y, 0f);
            GL.Vertex3(pts[6].x, pts[6].y, 0f);
            GL.Vertex3(pts[4].x, pts[4].y, 0f);

            GL.End();
            GL.PopMatrix();
        }
    }
}
