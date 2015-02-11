f = open("test2.csv","r")
for line in f:
	if len(line) > 5:
		tokens = line.split(',')
		name = tokens[1]
		name = name.rstrip()
		addresses = tokens[2].split('-')
		from1 = addresses[0]
		to = addresses[1]
		newline = '<Parameter name="' + name + '" from="' + from1 + '" to="' + to +'">'
		print newline
