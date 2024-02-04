ALTER TABLE dogs ADD CONSTRAINT fk_owner FOREIGN KEY (owner_id) REFERENCES owners(id);
