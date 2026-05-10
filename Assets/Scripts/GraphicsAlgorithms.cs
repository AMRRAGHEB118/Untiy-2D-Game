using UnityEngine;
using System.Collections.Generic;

public class GraphicsAlgorithms : MonoBehaviour
{
    // This script implements the required graphics algorithms as utility functions
    // which can be used to draw on a Texture2D or for other calculations.

    // 1. Line DDA
    public static List<Vector2Int> DrawLineDDA(Vector2Int p1, Vector2Int p2)
    {
        List<Vector2Int> points = new List<Vector2Int>();
        int dx = p2.x - p1.x;
        int dy = p2.y - p1.y;
        int steps = Mathf.Max(Mathf.Abs(dx), Mathf.Abs(dy));

        float xInc = dx / (float)steps;
        float yInc = dy / (float)steps;

        float x = p1.x;
        float y = p1.y;

        for (int i = 0; i <= steps; i++)
        {
            points.Add(new Vector2Int(Mathf.RoundToInt(x), Mathf.RoundToInt(y)));
            x += xInc;
            y += yInc;
        }
        return points;
    }

    // 2. Line Bresenham's
    public static List<Vector2Int> DrawLineBresenham(Vector2Int p1, Vector2Int p2)
    {
        List<Vector2Int> points = new List<Vector2Int>();
        int x = p1.x;
        int y = p1.y;
        int dx = Mathf.Abs(p2.x - p1.x);
        int dy = Mathf.Abs(p2.y - p1.y);
        int sx = p1.x < p2.x ? 1 : -1;
        int sy = p1.y < p2.y ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            points.Add(new Vector2Int(x, y));
            if (x == p2.x && y == p2.y) break;
            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err -= dy;
                x += sx;
            }
            if (e2 < dx)
            {
                err += dx;
                y += sy;
            }
        }
        return points;
    }

    // 3. Circle (Midpoint Algorithm)
    public static List<Vector2Int> DrawCircle(Vector2Int center, int radius)
    {
        List<Vector2Int> points = new List<Vector2Int>();
        int x = 0;
        int y = radius;
        int d = 3 - 2 * radius;

        AddCirclePoints(center, x, y, points);
        while (y >= x)
        {
            x++;
            if (d > 0)
            {
                y--;
                d = d + 4 * (x - y) + 10;
            }
            else
            {
                d = d + 4 * x + 6;
            }
            AddCirclePoints(center, x, y, points);
        }
        return points;
    }

    private static void AddCirclePoints(Vector2Int center, int x, int y, List<Vector2Int> points)
    {
        points.Add(new Vector2Int(center.x + x, center.y + y));
        points.Add(new Vector2Int(center.x - x, center.y + y));
        points.Add(new Vector2Int(center.x + x, center.y - y));
        points.Add(new Vector2Int(center.x - x, center.y - y));
        points.Add(new Vector2Int(center.x + y, center.y + x));
        points.Add(new Vector2Int(center.x - y, center.y + x));
        points.Add(new Vector2Int(center.x + y, center.y - x));
        points.Add(new Vector2Int(center.x - y, center.y - x));
    }

    // 4. Ellipse (Midpoint Algorithm)
    public static List<Vector2Int> DrawEllipse(Vector2Int center, int rx, int ry)
    {
        List<Vector2Int> points = new List<Vector2Int>();
        float dx, dy, d1, d2, x, y;
        x = 0;
        y = ry;

        d1 = (ry * ry) - (rx * rx * ry) + (0.25f * rx * rx);
        dx = 2 * ry * ry * x;
        dy = 2 * rx * rx * y;

        while (dx < dy)
        {
            AddEllipsePoints(center, (int)x, (int)y, points);
            if (d1 < 0)
            {
                x++;
                dx = dx + (2 * ry * ry);
                d1 = d1 + dx + (ry * ry);
            }
            else
            {
                x++;
                y--;
                dx = dx + (2 * ry * ry);
                dy = dy - (2 * rx * rx);
                d1 = d1 + dx - dy + (ry * ry);
            }
        }

        d2 = ((ry * ry) * ((x + 0.5f) * (x + 0.5f))) + ((rx * rx) * ((y - 1) * (y - 1))) - (rx * rx * ry * ry);

        while (y >= 0)
        {
            AddEllipsePoints(center, (int)x, (int)y, points);
            if (d2 > 0)
            {
                y--;
                dy = dy - (2 * rx * rx);
                d2 = d2 + (rx * rx) - dy;
            }
            else
            {
                y--;
                x++;
                dx = dx + (2 * ry * ry);
                dy = dy - (2 * rx * rx);
                d2 = d2 + dx - dy + (rx * rx);
            }
        }
        return points;
    }

    private static void AddEllipsePoints(Vector2Int center, int x, int y, List<Vector2Int> points)
    {
        points.Add(new Vector2Int(center.x + x, center.y + y));
        points.Add(new Vector2Int(center.x - x, center.y + y));
        points.Add(new Vector2Int(center.x + x, center.y - y));
        points.Add(new Vector2Int(center.x - x, center.y - y));
    }

    // 5. Transformations
    public static Vector2 Translate(Vector2 point, Vector2 translation)
    {
        return point + translation;
    }

    public static Vector2 Scale(Vector2 point, Vector2 scale)
    {
        return new Vector2(point.x * scale.x, point.y * scale.y);
    }

    public static Vector2 Rotate(Vector2 point, float angleDegrees)
    {
        float angleRad = angleDegrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(angleRad);
        float sin = Mathf.Sin(angleRad);
        return new Vector2(
            point.x * cos - point.y * sin,
            point.x * sin + point.y * cos
        );
    }
}
