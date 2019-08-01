package com.thinktecture.boardist.webApi.models;

import java.util.UUID;

import io.quarkus.hibernate.orm.panache.PanacheEntityBase;

public abstract class SyncableInterface extends PanacheEntityBase {

    public UUID id;
    public byte[] rowVersion;
    public boolean isDeleted;
}