package com.thinktecture.boardist.webApi.models;

public abstract class ItemInterface extends SyncableInterface {
    public String name;
    public int boardGameGeekId;

    public void copyOver( ItemInterface other) {
        // TODO check correct copy behauvior --> also remove values by null or not?
        this.name = other.name;
        this.isDeleted = other.isDeleted;
        this.rowVersion = other.rowVersion;
        this.boardGameGeekId = other.boardGameGeekId;
        this.id = other.id;
    }
}
