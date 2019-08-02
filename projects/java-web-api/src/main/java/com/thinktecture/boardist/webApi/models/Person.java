package com.thinktecture.boardist.webApi.models;

import java.util.UUID;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.Table;

import org.hibernate.annotations.GenericGenerator;

@Entity()
@Table(name = "Persons", schema = "dbo", catalog = "boardist")
public class Person extends ItemInterface {
    

    // values have to be redefined, so that panache recognises them corretly. The Interfaces have no value
    // but to have generic types
    @Id
    @GenericGenerator(name = "uuid", strategy = "uuid")
    @GeneratedValue(generator = "uuid")
    @Column(columnDefinition = "UNIQUEIDENTIFIER")
    public UUID id;
    
    public String name;
    public int boardGameGeekId;
    public char[] rowVersion;
    public boolean isDeleted;


}
