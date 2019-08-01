package com.thinktecture.boardist.webApi.endpoints;

import java.util.List;
import java.util.stream.Collectors;

import javax.ws.rs.Consumes;
import javax.ws.rs.DELETE;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.PUT;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;

import io.quarkus.hibernate.orm.panache.PanacheEntityBase;
import io.quarkus.panache.common.Page;

@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class BaseEndpoint<T extends PanacheEntityBase> {

    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public List<PanacheEntityBase> getPersons() {
        return T.findAll().page(Page.ofSize(20)).stream().collect(Collectors.toList());
    }

    @GET
    @Path("{id}")
    @Produces(MediaType.APPLICATION_JSON)
    public PanacheEntityBase getPerson(@PathParam("id") String id) {
        return T.findById(id);
    }

    @DELETE
    @Path("{id}")
    public void deletePerson(@PathParam("id") String id) {
        T.findById(id).delete();
    }

    @POST
    public void postPerson(T data) {
        data.persistAndFlush();
    }

    @PUT
    public void updatePerson(T data) {
        // TODO BeanUtils copyProperties
    }

    @GET
    @Path("sync")
    public void sync() {
        // TODO sync
    }
}