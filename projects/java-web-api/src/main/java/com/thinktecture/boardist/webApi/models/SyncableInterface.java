package com.thinktecture.boardist.webApi.models;

import java.sql.Date;
import java.util.UUID;

import io.quarkus.hibernate.orm.panache.PanacheEntityBase;

public abstract class SyncableInterface extends PanacheEntityBase {

    public UUID id;
    public char[] rowVersion;
    public boolean isDeleted;
}