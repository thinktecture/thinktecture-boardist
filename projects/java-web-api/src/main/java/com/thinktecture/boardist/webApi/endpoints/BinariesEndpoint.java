package com.thinktecture.boardist.webApi.endpoints;

import java.io.IOException;
import java.util.UUID;

import javax.inject.Inject;
import javax.ws.rs.Consumes;
import javax.ws.rs.FormParam;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

import com.thinktecture.boardist.core.FileLoadResult;
import com.thinktecture.boardist.webApi.FileCategoryEnum;
import com.thinktecture.boardist.webApi.dtos.BinaryUploadDto;
import com.thinktecture.boardist.webApi.services.FileService;

@Path("binaries")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class BinariesEndpoint {

    @Inject
    FileService fileService;

    @GET
    @Path("{id}/logo")
    public Response getLogo(@PathParam("id") UUID id) throws IOException {

        FileLoadResult flr = this.fileService.load(id, FileCategoryEnum.LOGO);
        return Response.ok(flr.getStream().readAllBytes(), flr.getMimeType()).build();
    }

    @GET
    @Path("{id}/rules")
    public Response getRules(@PathParam("id") UUID id) throws IOException {

        FileLoadResult flr = this.fileService.load(id, FileCategoryEnum.RULES);
        return Response.ok(flr.getStream().readAllBytes(), flr.getMimeType()).build();
    }

    @POST
    @Path("logo")
    public Response uploadLogo(BinaryUploadDto data) {
        upload(data, FileCategoryEnum.LOGO);
        return null;
    }

    @POST
    @Path("rules")
    public Response uploadRules(BinaryUploadDto data) {
        upload(data, FileCategoryEnum.RULES);
        return null;
    }

    void upload(BinaryUploadDto data, FileCategoryEnum fc) {
        System.out.println("UPLOAD " + fc.getCategoryValue());
        this.fileService.save(data.getFile(), data.getId(),fc.getCategoryValue(), "png");
    }
}
