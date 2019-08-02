package com.thinktecture.boardist.webApi.services;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.UUID;

import javax.enterprise.context.RequestScoped;
import javax.inject.Inject;
import javax.inject.Named;
import javax.ws.rs.core.MediaType;

import com.thinktecture.boardist.core.FileLoadResult;
import com.thinktecture.boardist.core.annotations.Property;
import com.thinktecture.boardist.webApi.FileCategoryEnum;

@Named
@RequestScoped
public class FileService {

    @Inject
    @Property // https://dzone.com/articles/how-to-inject-property-file-properties-with-cdi
    private String baseDir;

    @Inject
    @Property
    private String noImageAvailableFileName;

    public void save(FileInputStream stream, UUID gameId, FileCategoryEnum category, String fileExtension) {

        if (!fileExtension.startsWith(".")) {
            fileExtension = "." + fileExtension;
        }

        String fileIdName = gameId.toString();
        String fileName = category.getCategoryValue() + fileExtension;

        this.ensureFolderExists(Path.of(this.baseDir, fileIdName));

        File file = Path.of(this.baseDir, fileIdName, fileName).toFile();
        try {

            FileOutputStream out = new FileOutputStream(file);
            stream.transferTo(out);
        } catch (FileNotFoundException e) {
        } catch (IOException ioe) {

        }

        // TODO how to check in java -> what should it do?
        // var dbGame = await _boardistContext.Games.SingleAsync(p => p.Id == gameId);
        // _boardistContext.Entry(dbGame).Property(p => p.BoardGameGeekId).IsModified =
        // true;
        // await _boardistContext.SaveChangesAsync();
    }

    public void save(FileInputStream stream, UUID gameId, FileCategoryEnum category, MediaType contentType) {
        this.save(stream, gameId, category, contentType.getType());
    }

    public FileLoadResult load(UUID fileId, FileCategoryEnum category) {
        String fileIdName = fileId.toString();
        String fileToLoad = this.getSingleFile(category, fileIdName);

        if (fileToLoad.equals("")) {
            fileToLoad = Path.of(this.baseDir, noImageAvailableFileName).toString();
        }

        try {

            String mimeType = Files.probeContentType(Path.of(fileToLoad));
            FileInputStream fileStream = new FileInputStream(fileToLoad);
            return new FileLoadResult(fileStream, mimeType);

        } catch (FileNotFoundException fe) {
            return null;
        }  catch (IOException fe) {
            return null;
        }

    }

    public boolean exists(UUID fileId, FileCategoryEnum category) {
        String fileIdName = fileId.toString();
        String fileToLoad = getSingleFile(category, fileIdName);
        return Path.of(fileToLoad).toFile().exists();
    }

    public void delete(UUID fileId) {
        Path path = Path.of(this.baseDir, fileId.toString());
        if (path.toFile().exists()) {
            path.toFile().delete();
        }
    }

    private String getSingleFile(FileCategoryEnum fileCategory, String fileIdName) {
        String ret = "";

        Path path = Path.of(baseDir, fileIdName);
        if (!path.toFile().exists()) {
            return ret;
        }

        File f = path.toFile();
        if (f.isDirectory()) {
            File[] files = f.listFiles();
            for (File ff : files) {
                if (ff.getName().indexOf(fileCategory.getCategoryValue()) != -1) {
                    // TODO check if correct output format
                    ret = ff.getAbsolutePath();
                }
            }
        }

        return ret.toString();
    }

    private void ensureFolderExists(Path folder) {
        if (!folder.toFile().exists()) {
            folder.toFile().mkdirs();
        }
    }
}