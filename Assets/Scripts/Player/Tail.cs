using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;

public class Tail : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int length;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private float trailSpeed;

    [SerializeField] private Transform targetDir;
    [SerializeField] private float targetDist;

    private Vector3[] segmentV;
    private Vector3[] segmentPoses;

    [SerializeField] private PhotonView PV;

    private void Awake()
    {
        lineRenderer.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];
    }

    private void Update()
    {
        if (!PV.IsMine) // чтобы не влиять на других игроков
            return;

        DrawTail();
    }

    private void DrawTail()
    {
        segmentPoses[0] = targetDir.position;

        for (int i = 1; i < segmentPoses.Length; i++)
        {
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i - 1] + targetDir.right * targetDist, ref segmentV[i], smoothSpeed + i / trailSpeed);
        }

        lineRenderer.SetPositions(segmentPoses);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(length);

            stream.SendNext(targetDir.position);

            //segmentPoses[0] = targetDir.position;
            // stream.SendNext(segmentPoses[0]);

            for (int i = 1; i < segmentPoses.Length; i++)
            {
                segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i - 1] + targetDir.right * targetDist, ref segmentV[i], smoothSpeed + i / trailSpeed);

                stream.SendNext(segmentPoses[i]);
            }

            lineRenderer.SetPositions(segmentPoses);
        }
        else if (stream.IsReading)
        {
            length = (int)stream.ReceiveNext();

            segmentPoses[0] = (Vector3)stream.ReceiveNext(); //ошибка

            for (int i = 1; i < segmentPoses.Length; i++)
            {
                segmentPoses[i] = (Vector3)stream.ReceiveNext();
            }

            lineRenderer.SetPositions(segmentPoses);
        }
    }
}
