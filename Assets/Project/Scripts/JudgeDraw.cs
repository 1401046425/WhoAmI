
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class JudgeDraw : MonoBehaviour {
    // 画圆的精度点数，值越高要求画的越圆
    public int point = 6;
    // 画圆的精度差值，值越低要求画的越圆
    public float num = 100f;
    float beginTimer= 0;
    float betweenTimer = 0;
    Vector2 beginPos;
    Vector2 endPos;
    Vector2 betweenPos;
    List<float> posFloat;
    List<Vector2> posVector2;
    int maxIndex = 0;
    int i = 0;
    bool isBool = false;
    void Start(){
    	posFloat = new List<float>();
        posVector2 = new List<Vector2>();
    }
	// Update is called once per frame
	void Update () {
 
        // 开始坐标和结束坐标小于某个值
        // 取得开始点和最远点的半径 以及他们的中间坐标
        // 将取得的点与中间坐标相比小于某个差值
        if (Input.GetMouseButtonDown(0))
        {
            // 记录画圆的开始点坐标
            beginPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            beginTimer += Time.deltaTime;
            // 1.2秒之内计算点
            if (beginTimer > 1.2f)
            {
                beginTimer = 0f;
            }
            else
            {
                betweenTimer += Time.deltaTime;
                // 每隔0.1秒取点
                if (betweenTimer >= 0.1f)
                {
                    posVector2.Add(Input.mousePosition);
                    float f = Vector2.Distance(beginPos, Input.mousePosition);
                    posFloat.Add(f);
                    betweenTimer = 0f;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            // 画圆的结束点
            endPos = Input.mousePosition;
            beginTimer = 0f;
            betweenTimer = 0f;
            // 找到这些点最大值的索引和坐标
            for (int i = 1; i < posFloat.Count; i++)
            {
                if (posFloat[i] > posFloat[maxIndex])
                {
                    maxIndex = i;
                }
            }
            // 开始坐标和最远点的半径，以此作为判断依据
            float radius = posFloat[maxIndex] / 2;
            print("radius" + radius);
            // 开始坐标和最远点的中间坐标 
            betweenPos = (beginPos + posVector2[maxIndex])/2;
            foreach (var item in posVector2)
            {
                print(Vector2.Distance(item, betweenPos));
                // 判断获得的坐标和中间坐标的距离 和半径相减的绝对值 是不是小于一定范围
                if (Mathf.Abs(Vector2.Distance(item, betweenPos)-radius) < num)
                {
                    i++;
                }
            }
            print("当有(默认为)6个点在半径差值范围内");
            if (i > point)
            {
                isBool = true;
 
            }
            if (Vector2.Distance(beginPos,endPos) <= radius && isBool)
            {
                print("你画了一个圆");
            }
            // 数据初始化
            i = 0;
            maxIndex = 0;
            posFloat.Clear();
            posVector2.Clear();
            isBool = false;
        }
	}
}