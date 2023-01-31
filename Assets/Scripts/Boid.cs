using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [Header("Set Dynamically")] public Rigidbody rigid;

    private Neighborhood neighborhood;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Awake()
    {
        neighborhood = GetComponent<Neighborhood>();
        rigid = GetComponent<Rigidbody>();
        // Выбрать случайную начальную позицию
        pos = Random.insideUnitSphere * Spawner.s.spawnRadius;
        // Выбрать случайную начальную скорость
        var vel = Random.onUnitSphere * Spawner.s.velocity;
        rigid.velocity = vel;
        LookAhead();

        // Окрасить птицу в случайный цвет, но не слишком темный
        var randColor = Color.black;
        while (randColor.r + randColor.g + randColor.b < 1.0f)
        {
            randColor = new Color(Random.value, Random.value, Random.value);
        }

        Renderer[] rends = gameObject.GetComponentsInChildren<Renderer>();
        foreach (var r in rends)
        {
            r.material.color = randColor;
        }

        TrailRenderer tRend = GetComponent<TrailRenderer>();
        tRend.material.SetColor("_TintColor", randColor);
    }

    void FixedUpdate()
    {
        var vel = rigid.velocity;
        var spn = Spawner.s;

        // ПРЕДОТВРАЩЕНИЕ СТОЛКНОВЕНИЙ - избегать близких соседей
        var velAvoid = Vector3.zero;
        var tooClosePos = neighborhood.avgClosePos;
        // Если получен вектор Vector3.zero, ничего предпринимать не надо
        if (tooClosePos != Vector3.zero)
        {
            velAvoid = pos - tooClosePos;
            velAvoid.Normalize();
            velAvoid *= spn.velocity;
        }

        // СОГЛАСОВАНИЕ СКОРОСТИ - попробовать согласовать скорость с соседями
        var velAlign = neighborhood.avgVel;
        // Согласование требуется, только если velAlign не равно Vector3.zero
        if (velAlign != Vector3.zero)
        {
            // Нас интересует только направление, поэтому нормализуем скорость velAlign.Normalize();
            // и затем преобразуем в выбранную скорость
            velAlign *= spn.velocity;
        }

        // КОНЦЕНТРАЦИЯ СОСЕДЕЙ - движение в сторону центра группы соседей
        var velCenter = neighborhood.avgPos;
        if (velCenter != Vector3.zero)
        {
            velCenter -= transform.position;
            velCenter.Normalize();
            velCenter *= spn.velocity;
        }

        // ПРИТЯЖЕНИЕ - организовать движение в сторону объекта Attractor
        var delta = Attractor.Pos - pos;

        // Проверить, куда двигаться, в сторону Attractor или от него
        var attracted = (delta.magnitude > spn.attractPushDist);
        var velAttract = delta.normalized * spn.velocity;

        // Применить все скорости
        var fdt = Time.fixedDeltaTime;

        if (velAvoid != Vector3.zero)
            vel = Vector3.Lerp(vel, velAvoid, spn.collAvoid * fdt);
        else
        {
            if (velAlign != Vector3.zero)
                vel = Vector3.Lerp(vel, velAlign, spn.velMatching * fdt);
            if (velCenter != Vector3.zero)
                vel = Vector3.Lerp(vel, velAlign, spn.flockCentering * fdt);
            if (velAttract != Vector3.zero)
                vel = Vector3.Lerp(vel, attracted ? velAttract : -velAttract, spn.attractPull * fdt);
        }


        // Установить vel в соответствии c velocity в объекте-одиночке Spawner
        vel = vel.normalized * spn.velocity;

        // В заключение присвоить скорость компоненту Rigidbody
        rigid.velocity = vel;

        // Повернуть птицу клювом в сторону нового направления движения
        LookAhead();
    }


    void LookAhead()
    {
        // Ориентировать птицу клювом в направлении полета
        transform.LookAt(pos + rigid.velocity);
    }

    public Vector3 pos
    {
        get => transform.position;
        set => transform.position = value;
    }
}