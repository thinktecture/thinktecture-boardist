package com.thinktecture.boardist.webApi.models;

import java.util.UUID;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.Table;

import org.hibernate.annotations.GenericGenerator;

@Entity()
@Table(name = "Mechanics", schema = "dbo", catalog = "boardist")
public class Mechanic extends Item {
    
    @Id
    @GenericGenerator(name = "uuid", strategy = "uuid2")
    @GeneratedValue(generator = "uuid")
    @Column(columnDefinition = "UNIQUEIDENTIFIER")
    public UUID id;
    
    public String name;
    public int boardGameGeekId;
    public byte[] rowVersion;
    public boolean isDeleted;

}
