package com.thinktecture.boardist.webApi.models;

import java.util.UUID;

import com.thinktecture.boardist.webApi.dtos.ItemDto;

import io.quarkus.hibernate.orm.panache.PanacheEntityBase;

public class Item extends PanacheEntityBase implements SyncableInterface {
    
    public UUID id;
    public String name;
    public int boardGameGeekId;
    public char[] rowVersion;
    public Boolean isDeleted;

    public void copyOver( ItemDto other) {
        this.name = other.name;
        this.boardGameGeekId = other.boardGameGeekId;
        this.id = other.id;
    }
}
