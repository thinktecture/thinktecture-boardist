package com.thinktecture.boardist.webApi.endpoints;

import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

import javax.transaction.Transactional;
import javax.ws.rs.Consumes;
import javax.ws.rs.DELETE;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.PUT;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

import com.thinktecture.boardist.webApi.models.Person;

import io.quarkus.hibernate.orm.panache.PanacheEntityBase;
import io.quarkus.panache.common.Page;

@Path("persons")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class PersonEndpoint {

    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public List<PanacheEntityBase> getPersons() {
        return Person.findAll().list();
    }

    @GET
    @Path("{id}")
    @Produces(MediaType.APPLICATION_JSON)
    public Response getPerson(@PathParam("id") UUID id) {

        Person person = Person.findById(id);
        if (person == null) {
            return Response.status(404).build();
        }

        return Response.ok(Person.findById(id)).build();
    }

    @DELETE
    @Path("{id}")
    @Transactional()
    public Response deletePerson(@PathParam("id") UUID id) {
        Person delperson = Person.findById(id);
        if (delperson == null) {
            return Response.status(404).build();
        }
        if (delperson.isPersistent()) {
            delperson.delete();
        }
        return Response.ok().build();
    }

    @POST
    public Response postPerson(Person data) {
        data.persistAndFlush();
        return Response.ok(data).build();
    }

    @PUT
    public void updatePerson(Person data) {
        // preferred method would be BeanUtils.copyProperties
        Person old = Person.findById(data.id);
        old.copyOver(data);
        old.persistAndFlush();
    }

    @GET
    @Path("sync")
    public void sync() {
        // TODO sync
    }
}