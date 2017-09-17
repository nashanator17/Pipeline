using UnityEngine;
using Mapbox.Map;
using Mapbox.Unity;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using System;

public class RasterMap : MonoBehaviour, Mapbox.Utils.IObserver<RasterTile> {
    private Map<RasterTile> map;
    public Vector2d subtractFromInit;
    public float tileSizeInMeters;
    void Start() {
        map = new Map<RasterTile>(MapboxAccess.Instance);
        map.MapId = "mapbox://styles/mapbox/streets-v10";
        map.Subscribe(this);
        MoveToPosition(new Vector2d(49.2320, -123.1164));
    }

    void MoveToPosition(Vector2d baseLoc) {
        foreach (Transform child in transform) {
            GameObject.Destroy(child);
        }
        var boundsize = 0.0005;
        var zoom = 18;
        tileSizeInMeters = Conversions.GetTileScaleInMeters((float)baseLoc.x, zoom) * 256;
        subtractFromInit = Conversions.LatitudeLongitudeToTileId((float)baseLoc.x, (float)baseLoc.y, zoom);
        map.SetVector2dBoundsZoom(new Vector2dBounds(new Vector2d(baseLoc.x - boundsize, baseLoc.y - boundsize),
                new Vector2d(baseLoc.x + boundsize, baseLoc.y + boundsize)), zoom);
        map.Update();
    }

    public void OnNext(RasterTile tile) {
        if (tile.CurrentState == Tile.State.Loaded) {
            if (tile.HasError) {
                Debug.Log("RasterMap: " + tile.ExceptionsAsString);
                return;
            }

            var tileQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            tileQuad.transform.SetParent(transform);
            tileQuad.name = tile.Id.ToString();
            float scaleFactor = tileSizeInMeters;
            tileQuad.transform.localPosition = new Vector3((tile.Id.X - (int)(subtractFromInit.x))* scaleFactor, -(tile.Id.Y - (int)(subtractFromInit.y)) * scaleFactor, 0);
            tileQuad.transform.localRotation = Quaternion.identity;
            tileQuad.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
            var texture = new Texture2D(0, 0);
            texture.LoadImage(tile.Data);
            var material = new Material(Shader.Find("Unlit/Texture"));
            material.mainTexture = texture;
            tileQuad.GetComponent<MeshRenderer>().sharedMaterial = material;
        }
    }
}
